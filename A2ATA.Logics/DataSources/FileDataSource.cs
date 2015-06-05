using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using A2ATA.Logics.Contracts;
using A2ATA.Logics.Contracts.Offers;
using A2ATA.Logics.Money;
using Newtonsoft.Json;

namespace A2ATA.Logics.DataSources
{
    public class FileDataSource : DataSource, IOfferRegistrator, IContractRegistrator, IDisposable
    {
        internal class Structure
        {
            internal List<Contract> Contracts { get; set; }

            internal List<Offer> Offers { get; set; }

            internal List<Actor> Actors { get; set; }

            internal Structure()
            {
                this.Contracts = new List<Contract>();
                this.Offers = new List<Offer>();
                this.Actors = new List<Actor>();
            }
        }

        private readonly Structure structure;

        private readonly FileStream fileLock;

        private readonly JsonSerializerSettings jsonSerializerSettings =
            new JsonSerializerSettings {TypeNameHandling = TypeNameHandling.All};

        public string FileName { get; private set; }

        public FileDataSource(string fileName)
        {
            if (File.Exists(fileName))
            {
                this.structure = JsonConvert.DeserializeObject<Structure>(
                    File.ReadAllText(fileName),
                    this.jsonSerializerSettings
                    );
                this.fileLock = File.OpenWrite(fileName);
            }
            else
            {
                this.structure = new Structure();
                this.fileLock = File.Create(fileName);
            }

            this.FileName = fileName;
        }


        public static FileDataSource Load()
        {
            var appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            var ata = Path.Combine(appData, "a2ata");
            if (!Directory.Exists(ata))
                Directory.CreateDirectory(ata);
            return new FileDataSource(Path.Combine(ata, "store.json"));
        }

        public override IEnumerable<Contract> EnumerateContracts()
        {
            return this.structure.Contracts;
        }

        public override IEnumerable<Offer> EnumerateOffers()
        {
            return this.structure.Offers;
        }

        public override IEnumerable<Actor> EnumerateActors()
        {
            return this.structure.Actors;
        }

        public string Register(Offer offer)
        {
            var newId = "offer." + Path.GetRandomFileName() + Path.GetRandomFileName();
            offer.Id = newId;
            this.structure.Offers.Add(offer);

            return offer.Id;
        }

        public string Register(Contract contract)
        {
            var newId = "contract." + Path.GetRandomFileName() + Path.GetRandomFileName();
            contract.Id = newId;
            this.structure.Contracts.Add(contract);

            return contract.Id;
        }

        public void Dispose()
        {
            this.fileLock.Close();
            this.fileLock.Dispose();

            var data = JsonConvert.SerializeObject(this.structure, this.jsonSerializerSettings);
            File.WriteAllText(this.FileName, data);
        }
    }
}
