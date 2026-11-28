using Catalog.Api.Models;
using Marten.Schema;

namespace Catalog.Api.Data;

public class SeedData : IInitialData
{
    public async Task Populate(IDocumentStore store, CancellationToken cancellation)
    {
        using var session = store.LightweightSession();
        if (await session.Query<Models.Products>().AnyAsync(cancellation))
            return;

        session.StoreObjects(Products);
        await session.SaveChangesAsync(cancellation);
    }
    private static readonly List<Models.Products> Products = new()
    {
        new Models.Products { Name = "Product 1", Price = 100,Category=new List<string>{"Category 1"},ImageFile="product1.jpg",Description="Description 1"},
        new Models.Products { Name = "Product 2", Price = 200,Category=new List<string>{"Category 2"},ImageFile="product2.jpg",Description="Description 2"},
        new Models.Products { Name = "Product 3", Price = 300,Category=new List<string>{"Category 3"},ImageFile="product3.jpg",Description="Description 3"},
        new Models.Products { Name = "Product 4", Price = 300,Category=new List<string>{"Category 4"},ImageFile="product4.jpg",Description="Description 4"},
    };
}
