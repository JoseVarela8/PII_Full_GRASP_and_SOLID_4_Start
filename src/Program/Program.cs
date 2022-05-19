//-------------------------------------------------------------------------
// <copyright file="Program.cs" company="Universidad Católica del Uruguay">
// Copyright (c) Programación II. Derechos reservados.
// </copyright>
//-------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;

namespace Full_GRASP_And_SOLID
{
    public class Program
    {
        private static List<Product> productCatalog = new List<Product>();

        private static List<Equipment> equipmentCatalog = new List<Equipment>();

        public static void Main(string[] args)
        {
            PopulateCatalogs();

            Recipe recipe = new Recipe();
            recipe.FinalProduct = GetProduct("Café con leche");
            
            //Saco estos porque aplico el cambio requerido para el patrón creator
            /*
            recipe.AddStep(new Step(GetProduct("Café"), 100, GetEquipment("Cafetera"), 120));
            recipe.AddStep(new Step(GetProduct("Leche"), 200, GetEquipment("Hervidor"), 60));
            */

            //CREATOR
            //LA CLASE RECIPE ES EXPERTA EN CREAR INSTANCIAS DE STEP
            recipe.AddStep(GetProduct("Café"), 100, GetEquipment("Cafetera"), 120);
            recipe.AddStep(GetProduct("Leche"), 200, GetEquipment("Hervidor"), 60);
            /*
            // Ejemplo Creator de la lectura
            
            sale.AddLineItem(new SalesLineItem(1, ProductAt(0)));
            sale.AddLineItem(new SalesLineItem(2, ProductAt(1)));
            sale.AddLineItem(new SalesLineItem(3, ProductAt(2)));

            sale.AddLineItem(1, ProductAt(0));
            sale.AddLineItem(2, ProductAt(1));
            sale.AddLineItem(3, ProductAt(2));
            */


            IPrinter printer;
            printer = new ConsolePrinter();
            printer.PrintRecipe(recipe);
            printer = new FilePrinter();
            printer.PrintRecipe(recipe);
        }

        private static void PopulateCatalogs()
        {
            AddProductToCatalog("Café", 100);
            AddProductToCatalog("Leche", 200);
            AddProductToCatalog("Café con leche", 300);

            AddEquipmentToCatalog("Cafetera", 1000);
            AddEquipmentToCatalog("Hervidor", 2000);
        }


        private static void AddProductToCatalog(string description, double unitCost)
        {
            productCatalog.Add(new Product(description, unitCost));
        }

        private static void AddEquipmentToCatalog(string description, double hourlyCost)
        {
            equipmentCatalog.Add(new Equipment(description, hourlyCost));
        }

        private static Product ProductAt(int index)
        {
            return productCatalog[index] as Product;
        }

        private static Equipment EquipmentAt(int index)
        {
            return equipmentCatalog[index] as Equipment;
        }

        private static Product GetProduct(string description)
        {
            var query = from Product product in productCatalog where product.Description == description select product;
            return query.FirstOrDefault();
        }

        private static Equipment GetEquipment(string description)
        {
            var query = from Equipment equipment in equipmentCatalog where equipment.Description == description select equipment;
            return query.FirstOrDefault();
        }
    }
}
