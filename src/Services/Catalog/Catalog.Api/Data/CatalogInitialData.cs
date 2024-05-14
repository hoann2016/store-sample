using Marten.Schema;

namespace Catalog.Api.Data;

public class CatalogInitialData:IInitialData
{
    public async Task Populate(IDocumentStore store, CancellationToken cancellation)
    {
        using var session=store.LightweightSession();
        if (await session.Query<Product>().AnyAsync())
        {
            return;
        }
        session.Store<Product>(GetPreconfiguredProducts());
        await session.SaveChangesAsync(cancellation);
    }

    private IEnumerable<Product> GetPreconfiguredProducts()
    {
       return new List<Product>()
       {
           new Product(){Name="Product1",Category=new List<string>(){"Category1"},Description="Description1",ImageFile="ImageFile1",Price=10},
           new Product(){Name="Product2",Category=new List<string>(){"Category2"},Description="Description2",ImageFile="ImageFile2",Price=20},
           new Product(){Name="Product3",Category=new List<string>(){"Category3"},Description="Description3",ImageFile="ImageFile3",Price=30},
           new Product(){Name="Product4",Category=new List<string>(){"Category4"},Description="Description4",ImageFile="ImageFile4",Price=40},
           new Product(){Name="Product5",Category=new List<string>(){"Category5"},Description="Description5",ImageFile="ImageFile5",Price=50},
           new Product(){Name="Product6",Category=new List<string>(){"Category6"},Description="Description6",ImageFile="ImageFile6",Price=60},
           new Product(){Name="Product7",Category=new List<string>(){"Category7"},Description="Description7",ImageFile="ImageFile7",Price=70},
           new Product(){Name="Product8",Category=new List<string>(){"Category8"},Description="Description8",ImageFile="ImageFile8",Price=80},
           new Product(){Name="Product9",Category=new List<string>(){"Category9"},Description="Description9",ImageFile="ImageFile9",Price=90},
           new Product(){Name="Product10",Category=new List<string>(){"Category10"},Description="Description10",ImageFile="ImageFile10",Price=100},

       };
       
    }
}