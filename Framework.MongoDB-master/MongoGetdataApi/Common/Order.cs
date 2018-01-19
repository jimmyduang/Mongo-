using FrameWork.MongoDB.MongoDbConfig;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MongoGetdataApi.Common
{
    [Mongo("OrderSys", "order")]
    public class Order : MongoEntity
    {
        public string orderId { get; set; }
        public GoodsInfo goodsInfo { get; set; }
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime OrderDateTime { get; set; }
        public int Count { get; set; }
        public double Total { get; set; }

    }
    [Mongo("OrderSys", "GoodsInfo")]
    public class GoodsInfo : MongoEntity
    {
        public string GoodsNo { get; set; }
        public string GoodsName { get; set; }
        public double Price { get; set; }

    }
}