using AttributeModel = TesteBackend.Models.Attribute;


namespace TesteBackend.Services
{
    public class AttributeService
    {
        private readonly TestDbContext _context;

        public AttributeService(TestDbContext context)
        {
            _context = context;
        }

        public IEnumerable<AttributeModel> GetAll()
        {
            return _context.Attributes.OrderBy(a => a.DateCreated).ToList();
        }

        public AttributeModel? GetById(int id)
        {
            return _context.Attributes.FirstOrDefault(a => a.Id == id);
        }

        public AttributeModel Create(AttributeModel attribute)
        {
            attribute.DateCreated = DateTime.UtcNow;
            _context.Attributes.Add(attribute);
            _context.SaveChanges();
            return attribute;
        }

        public void Delete(int id)
        {
            var attribute = _context.Attributes.Find(id) ?? throw new Exception("Attribute not found");
            _context.Attributes.Remove(attribute);
            _context.SaveChanges();
        }

        public void Update(int id, AttributeModel updatedAttribute)
        {
            var attribute = _context.Attributes.Find(id) ?? throw new Exception($"Attribute with ID {id} not found");

            if (!string.IsNullOrEmpty(updatedAttribute.Brand))
            {
                attribute.Brand = updatedAttribute.Brand;
            }

            if (!string.IsNullOrEmpty(updatedAttribute.Color))
            {
                attribute.Color = updatedAttribute.Color;
            }

            attribute.Origem = updatedAttribute.Origem;

            _context.SaveChanges();
        }

        public IEnumerable<AttributeModel> GetByIds(List<int> ids)
        {
            return _context.Attributes
                           .Where(a => ids.Contains(a.Id))
                           .OrderBy(a => a.DateCreated)
                           .ToList();
        }

        public IEnumerable<AttributeModel> GetByProductId(int productId)
        {
            return _context.ProductAttributes
                            .Where(pa => pa.ProductId == productId && pa.Attribute != null)
                            .Select(pa => pa.Attribute)
                            .ToList();
        }
    }
}
