using System;
using NHibernate;
using NHibernate.Cfg;
using NHibernateOracleApp.Entities; // Assuming Product.cs is in the Entities folder
using System.Linq; // For Linq queries example

namespace NHibernateOracleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("NHibernate Oracle App Demo");
            Console.WriteLine("--------------------------");

            ISessionFactory sessionFactory = null;

            try
            {
                // 1. Initialize NHibernate ISessionFactory
                Console.WriteLine("Initializing NHibernate SessionFactory...");
                var cfg = new Configuration();

                // Load configuration from hibernate.cfg.xml (ensure it's in the output directory)
                cfg.Configure(); // By default, looks for hibernate.cfg.xml

                // Add mappings from the assembly containing the Product class
                // This assumes Product.hbm.xml is an embedded resource in that assembly
                cfg.AddAssembly(typeof(Product).Assembly);
                // Alternatively, if you want to be very specific:
                // cfg.AddInputStream(NHibernate.HbmSerializer.Default.Serialize(typeof(Product)));

                sessionFactory = cfg.BuildSessionFactory();
                Console.WriteLine("NHibernate SessionFactory initialized successfully.");
                Console.WriteLine("--------------------------");

                // 2. Example Database Operations (Commented Out)
                // IMPORTANT: Ensure your hibernate.cfg.xml has correct database connection details
                // before uncommenting and running this section.
                Console.WriteLine("\n--- Example Database Operations (Currently Commented Out) ---");
                Console.WriteLine("To run these examples, uncomment the code below AND");
                Console.WriteLine("ensure 'hibernate.cfg.xml' is configured with your Oracle database details.");
                Console.WriteLine("--------------------------------------------------------------------");

                /*
                Console.WriteLine("\nAttempting to open session and perform operations...");
                using (ISession session = sessionFactory.OpenSession())
                {
                    using (ITransaction transaction = session.BeginTransaction())
                    {
                        try
                        {
                            // Create a new Product
                            Console.WriteLine("Creating and saving a new product...");
                            var newProduct = new Product { Name = "Sample Widget", Price = 19.99m };
                            session.Save(newProduct);
                            Console.WriteLine($"Saved product with ID: {newProduct.Id}");

                            // Commit the transaction
                            transaction.Commit();
                            Console.WriteLine("Transaction committed.");

                            // (Optional) Query for products
                            Console.WriteLine("\nQuerying for all products...");
                            using (ITransaction queryTransaction = session.BeginTransaction()) // Start a new transaction for read
                            {
                                var products = session.Query<Product>().ToList();
                                if (products.Any())
                                {
                                    foreach (var product in products)
                                    {
                                        Console.WriteLine($"Found Product: Id={product.Id}, Name='{product.Name}', Price={product.Price}");
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("No products found in the database.");
                                }
                                queryTransaction.Commit(); // Or Rollback if it's a read-only scenario and no changes intended
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Error during database operations: {ex.Message}");
                            Console.WriteLine($"Stack Trace: {ex.StackTrace}");
                            if (transaction.IsActive)
                            {
                                transaction.Rollback();
                                Console.WriteLine("Transaction rolled back due to error.");
                            }
                            // Propagate the exception if you want the application to halt or handle it further up
                            // throw;
                        }
                    }
                }
                Console.WriteLine("Example database operations section finished.");
                */

            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\n--- NHibernate Initialization Failed ---");
                Console.WriteLine($"Error during NHibernate ISessionFactory initialization: {ex.Message}");
                Console.WriteLine($"Stack Trace: {ex.StackTrace}");
                if (ex.InnerException != null)
                {
                    Console.WriteLine($"Inner Exception: {ex.InnerException.Message}");
                    Console.WriteLine($"Inner Stack Trace: {ex.InnerException.StackTrace}");
                }
                Console.ResetColor();
                Console.WriteLine("\nPlease ensure 'hibernate.cfg.xml' is correctly configured and in the output directory,");
                Console.WriteLine("and that the Oracle database is accessible with correct credentials.");
            }
            finally
            {
                // 4. Dispose ISessionFactory
                if (sessionFactory != null && !sessionFactory.IsClosed)
                {
                    Console.WriteLine("\nClosing NHibernate SessionFactory...");
                    sessionFactory.Close();
                    sessionFactory.Dispose();
                    Console.WriteLine("SessionFactory closed.");
                }
            }

            Console.WriteLine("\n--------------------------");
            Console.WriteLine("End of NHibernate Oracle App Demo. Press any key to exit.");
            Console.ReadKey();
        }
    }
}
