using System;
using System.Text;
using DatabaseFirstEx.Model;

namespace Chinook.EF
{
    class Program
    {
        public static void Main(string[]args)
        {
            Console.WriteLine("This program is running....");
            Repository tempRepo = new Repository();
            tempRepo.AddNewPlace();
            Console.WriteLine(tempRepo.ListAllPlaces());
        }
    }

    class Repository
    {
        // fields
        private readonly SchoolContext _context = new SchoolContext();
        // constructor
        public Repository()
        {}

        // methods
        public string ListAllPlaces()
        {
            var sb = new StringBuilder();
            foreach( var place in _context.Places)
            {
                sb.AppendLine($"{place.Id}: {place.Name}");
            }
            return sb.ToString();
        }

        public void AddNewPlace()
        {
            var newPlace = new Place
            {
                Id = 47,
                Name = "James",
                Description = "Aaron",
                Language = 3,
                BiomType = 1
            };

            _context.Add(newPlace);
            _context.SaveChanges();
        }

        public void RemovePlace()
        {
            var place = (Place)_context.Places.Where(c => c.Id == 47).First();
            _context.Places.Remove(place);
            _context.SaveChanges();
        }

    }
}
