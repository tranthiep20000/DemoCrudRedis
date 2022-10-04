using DemoMVC.Models;
using ServiceStack.Redis;

namespace DemoMVC.Repositories
{
    /// <summary>
    /// Imformation of CategoryRepository
    /// CreatedBy: ThiepTT(30/09/2022)
    /// </summary>
    public class CategoryRepository : IBaseRepository<Category>
    {
        #region Feild

        public string key = "id_c_";
        private IConfiguration _configuracoes;
        private string _conexao { get { return _configuracoes.GetConnectionString("RedisServer"); } }
        private RedisManagerPool _pool;

        #endregion Feild

        #region Contructor

        public CategoryRepository(IConfiguration config)
        {
            _configuracoes = config;
            _pool = new RedisManagerPool(_conexao);
        }

        #endregion Contructor

        #region Method

        public int Create(Category category)
        {
            using (var client = _pool.GetClient())
            {
                client.Set<Category>(key + category.Id, category);
                return 1;
            }
        }

        public int Delete(int id)
        {
            using (var client = _pool.GetClient())
            {
                var obj = client.Remove(key + id);
                return 1;
            }
        }

        public IEnumerable<Category> GetAll()
        {
            using (var client = _pool.GetClient())
            {
                var keys = client.GetAllKeys();

                var listCategory = new List<Category>();
                foreach (var key in keys)
                {
                    listCategory.Add(client.Get<Category>(key));
                }
                return listCategory;
            }
        }

        public Category GetById(int id)
        {
            using (var client = _pool.GetClient())
            {
                var category = client.Get<Category>(key + id);
                return category;
            }
        }

        public List<Category> GetBySearchValue(string SearchValue)
        {
            using (var client = _pool.GetClient())
            {
                var keys = client.GetAllKeys();

                var listCategory = new List<Category>();
                foreach (var key in keys)
                {
                    var category = client.Get<Category>(key);
                    if (category.Name.ToLower().Trim().ToString().Contains(SearchValue.ToLower().Trim().ToString())
                        || category.Id.ToString().Trim().ToLower().Contains(SearchValue.ToLower().Trim().ToString())
                        || category.DisplayOrder.ToString().Trim().ToLower().Contains(SearchValue.ToLower().Trim().ToString()))
                    {
                        listCategory.Add(category);
                    }
                }

                return listCategory;
            }
        }

        public int Update(Category category)
        {
            using (var client = _pool.GetClient())
            {
                client.Set<Category>(key + category.Id, category);
                return 1;
            }
        }

        #endregion Method
    }
}