using AttributeModel = TesteBackend.Models.Attribute;

namespace TesteBackend.Services
{
    public class AttributeService
    {
        private readonly TestDbContext _context;
        private readonly LoggerService loggerService;

        public AttributeService(TestDbContext context, LoggerService loggerService)
        {
            _context = context;
            this.loggerService = loggerService;
        }

        public IEnumerable<AttributeModel> GetAll()
        {
            loggerService.LogInformation("Buscando todos os atributos.");
            var attributes = _context.Attributes.OrderBy(a => a.DateCreated).ToList();
            loggerService.LogInformation($"Retornando {attributes.Count} atributos.");
            return attributes;
        }

        public AttributeModel? GetById(int id)
        {
            loggerService.LogInformation($"Buscando atributo com ID: {id}");
            var attribute = _context.Attributes.FirstOrDefault(a => a.Id == id);

            if (attribute == null)
            {
                loggerService.LogWarning($"Atributo com ID {id} não encontrado.");
            }
            else
            {
                loggerService.LogInformation($"Atributo com ID {id} encontrado.");
            }

            return attribute;
        }

        public AttributeModel Create(AttributeModel attribute)
        {
            loggerService.LogInformation("Criando novo atributo.");

            attribute.DateCreated = DateTime.UtcNow;
            _context.Attributes.Add(attribute);
            _context.SaveChanges();

            loggerService.LogInformation($"Atributo criado com sucesso. ID: {attribute.Id}");
            return attribute;
        }

        public void Delete(int id)
        {
            loggerService.LogInformation($"Tentando deletar atributo com ID: {id}");
            var attribute = _context.Attributes.Find(id);

            if (attribute == null)
            {
                loggerService.LogWarning($"Atributo com ID {id} não encontrado para deletar.");
                throw new Exception("Attribute not found");
            }

            _context.Attributes.Remove(attribute);
            _context.SaveChanges();

            loggerService.LogInformation($"Atributo com ID {id} deletado com sucesso.");
        }

        public void Update(int id, AttributeModel updatedAttribute)
        {
            loggerService.LogInformation($"Tentando atualizar atributo com ID: {id}");
            var attribute = _context.Attributes.Find(id);

            if (attribute == null)
            {
                loggerService.LogWarning($"Atributo com ID {id} não encontrado para atualizar.");
                throw new Exception($"Attribute with ID {id} not found");
            }

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

            loggerService.LogInformation($"Atributo com ID {id} atualizado com sucesso.");
        }

        public IEnumerable<AttributeModel> GetByIds(List<int> ids)
        {
            loggerService.LogInformation($"Buscando atributos pelos IDs: {string.Join(", ", ids)}");
            var attributes = _context.Attributes
                                   .Where(a => ids.Contains(a.Id))
                                   .OrderBy(a => a.DateCreated)
                                   .ToList();

            loggerService.LogInformation($"Retornando {attributes.Count} atributos.");
            return attributes;
        }

        public List<AttributeModel?> GetAttributesByProductId(int productId)
        {
            loggerService.LogInformation($"Buscando atributos associados ao produto com ID: {productId}");
            var attributes = _context.ProductAttributes
                                   .Where(pa => pa.ProductId == productId && pa.Attribute != null)
                                   .Select(pa => pa.Attribute)
                                   .ToList();

            loggerService.LogInformation($"Retornando {attributes.Count} atributos para o produto com ID {productId}.");
            return attributes;
        }
    }
}