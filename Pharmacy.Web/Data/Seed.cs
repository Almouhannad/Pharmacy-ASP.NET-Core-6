using Microsoft.AspNetCore.Identity;
using Pharmacy.Core.Constants;
using Pharmacy.Core.Entities.General.Users;
using Pharmacy.Core.Entities.ViewModels.Requests.Categories;
using Pharmacy.Core.Entities.ViewModels.Requests.Ingredients;
using Pharmacy.Core.Entities.ViewModels.Requests.Medicines;
using Pharmacy.Core.Interfaces.IServices;

namespace Pharmacy.Web.Data
{
    public class Seed
    {
        public static async Task SeedUsersAndRolesAsync(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                #region Roles
                var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                if (!await roleManager.RoleExistsAsync(ApplicationUserRoles.Admin))
                    await roleManager.CreateAsync(new IdentityRole(ApplicationUserRoles.Admin));

                if (!await roleManager.RoleExistsAsync(ApplicationUserRoles.User))
                    await roleManager.CreateAsync(new IdentityRole(ApplicationUserRoles.User));

                #endregion

                #region Admin user
                var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

                string adminUserEmail = "admin@hiast.edu.sy";
                var adminUser = await userManager.FindByEmailAsync(adminUserEmail);

                if (adminUser == null)
                {
                    var newAdminUser = new ApplicationUser()
                    {
                        Name = "Admin",
                        UserName = adminUserEmail,
                        Email = adminUserEmail,
                        EmailConfirmed = true
                    };
                    await userManager.CreateAsync(newAdminUser, "Admin@123");
                    await userManager.AddToRoleAsync(newAdminUser, ApplicationUserRoles.Admin);
                }
                #endregion
            }
        }

        public static async Task SeedDatabase(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var _servicePool = serviceScope.ServiceProvider.GetRequiredService<IServicePool>();

                #region Categories
                if (_servicePool.Categories.GetAll().Count() == 0)
                {
                    var categories = new[]
                    {
                        "Analgesics",
                        "Antibiotics",
                        "Antihistamines",
                        "Antivirals",
                        "Beta Blockers",
                        "Bronchodilators",
                        "Corticosteroids",
                        "Diuretics",
                        "Muscle Relaxants",
                        "Nonsteroidal Anti-Inflammatory Drugs",
                        "Oral Anti-Diabetics",
                        "Proton Pump Inhibitors",
                        "Statins",
                        "Vitamins"
                    };

                    foreach (var category in categories)
                    {
                        var createCategoryRequest = new CreateCategoryRequest
                        {
                            Name = category
                        };
                        await _servicePool.Categories.Add(createCategoryRequest);
                    }
                }

                #endregion

                #region Ingredients
                if (_servicePool.Ingredients.GetAll().Count() == 0)
                {
                    var ingredients = new[]
                    {
                        new { Name = "Acetaminophen", Description = "Pain reliever and fever reducer" },
                        new { Name = "Ibuprofen", Description = "Nonsteroidal anti-inflammatory drug" },
                        new { Name = "Aspirin", Description = "Pain reliever and blood thinner" },
                        new { Name = "Amoxicillin", Description = "Antibiotic to treat bacterial infections" },
                        new { Name = "Azithromycin", Description = "Antibiotic to treat bacterial infections" },
                        new { Name = "Ciprofloxacin", Description = "Antibiotic to treat bacterial infections" },
                        new { Name = "Doxycycline", Description = "Antibiotic to treat bacterial infections" },
                        new { Name = "Metformin", Description = "Oral anti-diabetic medication" },
                        new { Name = "Lisinopril", Description = "Angiotensin-converting enzyme inhibitor" },
                        new { Name = "Simvastatin", Description = "Statins to lower cholesterol" },
                        new { Name = "Omeprazole", Description = "Proton pump inhibitor to treat heartburn" },
                        new { Name = "Prednisone", Description = "Corticosteroid to treat inflammation" },
                        new { Name = "Warfarin", Description = "Anticoagulant to prevent blood clots" },
                        new { Name = "Albuterol", Description = "Bronchodilator to treat asthma" }
                    };

                    foreach (var ingredient in ingredients)
                    {
                        var createIngredientRequest = new CreateIngredientRequest
                        {
                            Name = ingredient.Name,
                            Description = ingredient.Description
                        };
                        await _servicePool.Ingredients.Add(createIngredientRequest);
                    }
                }
                #endregion

                #region Medicines
                if (_servicePool.Medicines.GetAll().Count() == 0)
                {
                    var medicines = new[]
                    {
                        new { TradeName = "Tylenol", ScientificName = "Acetaminophen", Company = "Johnson & Johnson", Description = "Pain reliever and fever reducer", Form = "Tablet", Dose = 500m, CategoryId = 1 },
                        new { TradeName = "Advil", ScientificName = "Ibuprofen", Company = "Pfizer", Description = "Pain reliever and fever reducer", Form = "Capsule", Dose = 200m, CategoryId = 1 },
                        new { TradeName = "Amoxil", ScientificName = "Amoxicillin", Company = "GlaxoSmithKline", Description = "Antibiotic to treat bacterial infections", Form = "Capsule", Dose = 500m, CategoryId = 2 },
                        new { TradeName = "Zithromax", ScientificName = "Azithromycin", Company = "Pfizer", Description = "Antibiotic to treat bacterial infections", Form = "Tablet", Dose = 250m, CategoryId = 2 },
                        new { TradeName = "Cipro", ScientificName = "Ciprofloxacin", Company = "Bayer", Description = "Antibiotic to treat bacterial infections", Form = "Tablet", Dose = 500m, CategoryId = 2 },
                        new { TradeName = "Glucophage", ScientificName = "Metformin", Company = "Merck", Description = "Oral anti-diabetic medication", Form = "Tablet", Dose = 500m, CategoryId = 11 },
                        new { TradeName = "Zestril", ScientificName = "Lisinopril", Company = "AstraZeneca", Description = "Angiotensin-converting enzyme inhibitor", Form = "Tablet", Dose = 10m, CategoryId = 5 },
                        new { TradeName = "Zocor", ScientificName = "Simvastatin", Company = "Merck", Description = "Statins to lower cholesterol", Form = "Tablet", Dose = 20m, CategoryId = 13 },
                        new { TradeName = "Prilosec", ScientificName = "Omeprazole", Company = "AstraZeneca", Description = "Proton pump inhibitor to treat heartburn", Form = "Capsule", Dose = 20m, CategoryId = 12 },
                        new { TradeName = "Prednisone", ScientificName = "Prednisone", Company = "Pfizer", Description = "Corticosteroid to treat inflammation", Form = "Tablet", Dose = 20m, CategoryId = 7 },
                        new { TradeName = "Coumadin", ScientificName = "Warfarin", Company = "Bristol-Myers Squibb", Description = "Anticoagulant to prevent blood clots", Form = "Tablet", Dose = 5m, CategoryId = 14 },
                        new { TradeName = "Ventolin", ScientificName = "Albuterol", Company = "GlaxoSmithKline", Description = "Bronchodilator to treat asthma", Form = "Inhaler", Dose = 100m, CategoryId = 9 },
                        new { TradeName = "Aspirin", ScientificName = "Aspirin", Company = "Bayer", Description = "Pain reliever and blood thinner", Form = "Tablet", Dose = 325m, CategoryId = 1 },
                        new { TradeName = "Doxycycline", ScientificName = "Doxycycline", Company = "Pfizer", Description = "Antibiotic to treat bacterial infections", Form = "Capsule", Dose = 100m, CategoryId = 2 },
                        new { TradeName = "Lipitor", ScientificName = "Atorvastatin", Company = "Pfizer", Description = "Statins to lower cholesterol", Form = "Tablet", Dose = 20m, CategoryId = 13 },
                        new { TradeName = "Plavix", ScientificName = "Clopidogrel", Company = "Bristol-Myers Squibb", Description = "Antiplatelet to prevent blood clots", Form = "Tablet", Dose = 75m, CategoryId = 14 },
                        new { TradeName = "Singulair", ScientificName = "Montelukast", Company = "Merck", Description = "Leukotriene modifier to treat asthma", Form = "Tablet", Dose = 10m, CategoryId = 9 },
                        new { TradeName = "Nexium", ScientificName = "Esomeprazole", Company = "AstraZeneca", Description = "Proton pump inhibitor to treat heartburn", Form = "Capsule", Dose = 20m, CategoryId = 12 },
                        new { TradeName = "Prozac", ScientificName = "Fluoxetine", Company = "Eli Lilly", Description = "Selective serotonin reuptake inhibitor to treat depression", Form = "Capsule", Dose = 20m, CategoryId = 10 },
                        new { TradeName = "Zoloft", ScientificName = "Sertraline", Company = "Pfizer", Description = "Selective serotonin reuptake inhibitor to treat depression", Form = "Tablet", Dose = 50m, CategoryId = 10 },
                        new { TradeName = "Lexapro", ScientificName = "Escitalopram", Company = "Forest Laboratories", Description = "Selective serotonin reuptake inhibitor to treat depression", Form = "Tablet", Dose = 10m, CategoryId = 10 },
                        new { TradeName = "Crestor", ScientificName = "Rosuvastatin", Company = "AstraZeneca", Description = "Statins to lower cholesterol", Form = "Tablet", Dose = 10m, CategoryId = 13 },
                        new { TradeName = "Diovan", ScientificName = "Valsartan", Company = "Novartis", Description = "Angiotensin II receptor antagonist to treat high blood pressure", Form = "Tablet", Dose = 80m, CategoryId = 5 },
                        new { TradeName = "Cozaar", ScientificName = "Losartan", Company = "Merck", Description = "Angiotensin II receptor antagonist to treat high blood pressure", Form = "Tablet", Dose = 50m, CategoryId = 5 },
                        new { TradeName = "Norvasc", ScientificName = "Amlodipine", Company = "Pfizer", Description = "Calcium channel blocker to treat high blood pressure", Form = "Tablet", Dose = 5m, CategoryId = 5 },
                        new { TradeName = "Zetia", ScientificName = "Ezetimibe", Company = "Merck", Description = "Cholesterol absorption inhibitor to treat high cholesterol", Form = "Tablet", Dose = 10m, CategoryId = 13 },
                        new { TradeName = "Vytorin", ScientificName = "Ezetimibe/Simvastatin", Company = "Merck", Description = "Combination of cholesterol absorption inhibitor and statin to treat high cholesterol", Form = "Tablet", Dose = 20m, CategoryId = 13 },
                        new { TradeName = "Tricor", ScientificName = "Fenofibrate", Company = "Abbott", Description = "Fibrate to treat high triglycerides", Form = "Tablet", Dose = 145m, CategoryId = 13 },
                        new { TradeName = "Lopid", ScientificName = "Gemfibrozil", Company = "Pfizer", Description = "Fibrate to treat high triglycerides", Form = "Tablet", Dose = 600m, CategoryId = 13 },
                        new { TradeName = "Questran", ScientificName = "Cholestyramine", Company = "Bristol-Myers Squibb", Description = "Bile acid sequestrant to treat high cholesterol", Form = "Powder", Dose = 4m, CategoryId = 13 },
                        new { TradeName = "Welchol", ScientificName = "Colesevelam", Company = "Daiichi Sankyo", Description = "Bile acid sequestrant to treat high cholesterol", Form = "Tablet", Dose = 625m, CategoryId = 13 }

                    };

                    var medicineId = 1;
                    foreach (var medicine in medicines)
                    {
                        var createMedicineRequest = new CreateMedicineRequest
                        {
                            TradeName = medicine.TradeName,
                            ScientificName = medicine.ScientificName,
                            Company = medicine.Company,
                            Description = medicine.Description,
                            Form = medicine.Form,
                            Dose = medicine.Dose,
                            CategoryId = medicine.CategoryId
                        };
                        await _servicePool.Medicines.Add(createMedicineRequest);

                        var ingredients = new int[] { };
                        switch (medicineId)
                        {
                            case 1:
                                ingredients = new int[] { 1, 2, 6, 8, 9 };
                                break;
                            case 2:
                                ingredients = new int[] { 2, 3, 6, 11 };
                                break;
                            case 3:
                                ingredients = new int[] { 4, 5, 6, 8, 12 };
                                break;
                            case 4:
                                ingredients = new int[] { 5, 6, 13, 14 };
                                break;
                            case 5:
                                ingredients = new int[] { 6, 7, 1, 4};
                                break;
                            case 6:
                                ingredients = new int[] { 8, 9, 11, 14 };
                                break;
                            case 7:
                                ingredients = new int[] { 9, 10, 3, 1, 12, 13 };
                                break;
                            case 8:
                                ingredients = new int[] { 10, 11, 1, 5, 8 };
                                break;
                            case 9:
                                ingredients = new int[] { 11, 12 };
                                break;
                            case 10:
                                ingredients = new int[] { 12, 13 };
                                break;
                            case 11:
                                ingredients = new int[] { 13, 14 };
                                break;
                            case 12:
                                ingredients = new int[] { 1, 3, 5 };
                                break;
                            case 13:
                                ingredients = new int[] { 2, 4, 6 };
                                break;
                            case 14:
                                ingredients = new int[] { 7, 8, 9 };
                                break;
                            case 15:
                                ingredients = new int[] { 10, 11, 12 };
                                break;
                            case 16:
                                ingredients = new int[] { 13, 14, 1 };
                                break;
                            case 17:
                                ingredients = new int[] { 2, 3, 4 };
                                break;
                            case 18:
                                ingredients = new int[] { 5, 6, 7 };
                                break;
                            case 19:
                                ingredients = new int[] { 8, 9, 10 };
                                break;
                            case 20:
                                ingredients = new int[] { 11, 12, 13 };
                                break;
                            case 21:
                                ingredients = new int[] { 14, 1, 2 };
                                break;
                            case 22:
                                ingredients = new int[] { 3, 4, 5 };
                                break;
                            case 23:
                                ingredients = new int[] { 6, 7, 8 };
                                break;
                            case 24:
                                ingredients = new int[] { 9, 10, 11 };
                                break;
                            case 25:
                                ingredients = new int[] { 12, 13, 14 };
                                break;
                            case 26:
                                ingredients = new int[] { 1, 2, 3 };
                                break;
                            case 27:
                                ingredients = new int[] { 4, 5, 6 };
                                break;
                            case 28:
                                ingredients = new int[] { 7, 8, 9 };
                                break;
                            case 29:
                                ingredients = new int[] { 10, 11, 12 };
                                break;
                            case 30:
                                ingredients = new int[] { 13, 14, 1 };
                                break;
                        }

                        foreach (var ingredientId in ingredients)
                        {
                            var addIngredientToMedicineRequest = new AddIngredientToMedicineRequest
                            {
                                MedicineId = medicineId,
                                IngredientId = ingredientId,
                                Ratio = Random.Shared.NextInt64(1, 5)
                            };
                            await _servicePool.Medicines.AddIngredient(addIngredientToMedicineRequest);
                        }

                        medicineId++;
                    }
                }
                #endregion
            }
        }
    }
}
