using Framework.MongoDB.Model;
using FrameWork.Extension;
using FrameWork.MongoDB;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Monggg
{
    class Program
    {
        static void Main(string[] args)
        {
            //Task.Run(() =>
            //{
            //    Console.WriteLine(Add());

            //});

            //while (true){
            //    Add();
            //}

            var i = Console.ReadLine();
            Console.WriteLine(PageList(DateTime.Now.AddHours(-5),int .Parse(i)).Total);

          


        }
        static PageList<Order> PageList(DateTime Date,int i)
        {
            return new MongoDbService().PageList<Order>(a => true);

        }

        public static async Task<List<Order>> GetItemAsync<Order>(Expression<Func<Order, bool>> predicate,int pageindex,int pagesize) {
            MongoClient _mongoClient;
            string _connString = "MongoDb".ValueOfAppSetting();
            _mongoClient = new MongoClient(_connString);
            var db = _mongoClient.GetDatabase("OrderSys");
            var coll = db.GetCollection<Order>("order");
           

            var find = coll.Find(predicate);

            find = find.Skip((10 - 1) * 20).Limit(20);

            var items = await find.ToListAsync().ConfigureAwait(false);

            return items.ToList();
        }

        static void Add()
        {
            var id = Guid.NewGuid().ToString();
            string No = new Random().Next(1000, 9999).ToString();
            var _Price = int.Parse(No)%800;
            var _Count = new Random().Next(1, 10);
            try
            {
                var order = new Order
                {
                    _id = id,
                    orderId = DateTime.Now.ToString("yyyyMMddHHmmssfff") +No,
                    goodsInfo = new GoodsInfo
                    {
                        GoodsNo = No,
                        GoodsName = "goodsno-T" + No,
                        Price = _Price
                    },
                    Count = _Count,
                    Total = _Price * _Count,
                    OrderDateTime = DateTime.Now
                };
                new MongoDbService().Add(order);
                //return id;
            }
            catch (Exception)
            {

                throw;
            }
           
           
        }

    }
}
