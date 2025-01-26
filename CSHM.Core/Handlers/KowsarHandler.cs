using CSHM.Core.Handlers.Interfaces;
using CSHM.Presentation.Base;
using CSHM.Presentation.Kowsar;
using CSHM.Widget.Dapper;
using Dapper;


namespace CSHM.Core.Handlers
{
    public class KowsarHandler : IKowsarHandler
    {
        private readonly IDapperWidget _dapper;

        public KowsarHandler(IDapperWidget dapper)
        {
            _dapper = dapper;
        }

        public MessageViewModel InsertToGood(List<GoodViewModel> goodViewModel)
        {
            // Initialize result object
            MessageViewModel result = null;
            int successfulInserts = 0;
            int failedInserts = 0;

            // Validate input list
            if (goodViewModel == null || !goodViewModel.Any())
            {
                return new MessageViewModel
                {
                    Status = "success",
                    Message = "اتصال با موفقیت انجام شد"
                };
            }

            foreach (var item in goodViewModel)
            {
                // Validate GoodCode
                if (string.IsNullOrWhiteSpace(item.GoodCode) || !long.TryParse(item.GoodCode, out long goodCodeParsed) || goodCodeParsed <= 0)
                {
                    failedInserts++;
                    continue; // Skip invalid entries
                }

                try
                {
                    // Prepare parameters for the stored procedure
                    var parameters = new DynamicParameters();
                    parameters.Add("@goodCode", item.GoodCode);
                    parameters.Add("@goodName", item.GoodName);
                    parameters.Add("@goodTypeCode", item.GoodTypeCode);
                    parameters.Add("@maxSellPrice", item.MaxSellPrice);
                    parameters.Add("@minSellPrice", item.MinSellPrice);
                    parameters.Add("@stackAmount", item.StackAmount);
                    parameters.Add("@cachedBarCode", item.CachedBarCode);
                    parameters.Add("@active", item.Active);

                    // Call the stored procedure
                    var rowsAffected = _dapper.CallProcedure<int>("sp_insertToGood", parameters);

                    // Check the result
                    if (rowsAffected != null && rowsAffected.Any() && rowsAffected[0] > 0)
                    {
                        successfulInserts++;
                    }
                    else
                    {
                        failedInserts++;
                    }
                }
                catch (Exception ex)
                {
                    // Log the exception (consider using a logging framework like Serilog or NLog)
                    Console.WriteLine($"Error inserting good: {ex.Message}");
                    failedInserts++;
                }
            }

            // Determine the result based on the number of successful and failed inserts
            if (successfulInserts > 0)
            {
                result = new MessageViewModel
                {
                    Status = "Success",
                    Message = $"تغییرات بدرستی انجام شد. {successfulInserts}  کالا افزوده شد, {failedInserts}  کالا با شکست مواجهه شد."
                };
            }
            else
            {
                result = new MessageViewModel
                {
                    Status = "Error",
                    Message = goodViewModel.Any() ? $"کالایی جهت افزودن یافت نشد. {failedInserts}  کالا با شکست مواجهه شد." : "لطفا کد کالا معتبر وارد نمایید."
                };
            }

            return result;
        }





        public MessageViewModel DeleteGood(List<GoodViewModel> goodViewModel)
        {
            MessageViewModel result = null;
            int successfulInserts = 0;
            int failedInserts = 0;

            if (goodViewModel == null || !goodViewModel.Any())
            {
                return new MessageViewModel
                {
                    Status = "success",
                    Message = "اتصال با موفقیت انجام شد"
                };
            }


            int cnt = 0;

            var parameters = new DynamicParameters();
            foreach (var item in goodViewModel)
            {
                // Validate GoodCode
                if (string.IsNullOrWhiteSpace(item.GoodCode) || !long.TryParse(item.GoodCode, out long goodCodeParsed) || goodCodeParsed <= 0)
                {
                    failedInserts++;
                    continue; // Skip invalid entries
                }

                try
                {
                    // Prepare parameters for the stored procedure
                    parameters.Add("@goodCode", item.GoodCode);


                    // Call the stored procedure
                    var rowsAffected = _dapper.CallProcedure<int>("sp_deleteGood", parameters);

                    // Check the result
                    //if (rowsAffected != null && rowsAffected.Any() && rowsAffected[0] > 0)
                  //  {
                        successfulInserts++;
                //    }
                    //else
                    //{
                    //    failedInserts++;
                    //}
                }
                catch (Exception ex)
                {
                    // Log the exception (consider using a logging framework like Serilog or NLog)
                    Console.WriteLine($"Error Deleting good: {ex.Message}");
                    failedInserts++;
                }
            }
            if (successfulInserts > 0)
            {
                result = new MessageViewModel
                {
                    Status = "Success",
                    Message = $"تغییرات بدرستی انجام شد. {successfulInserts}  کالا حذف گردید, {failedInserts}  کالا با شکست مواجهه شد."
                };
            }
            else
            {
                result = new MessageViewModel
                {
                    Status = "Error",
                    Message = goodViewModel.Any() ? $"کالایی جهت حذف یافت نشد. {failedInserts}  کالا با شکست مواجهه شد." : "لطفا کد کالا معتبر وارد نمایید."
                };
            }

            return result;
        }



        public MessageViewModel Common(List<CommonViewModel> commonViewModel) 
        {
            // Initialize result object
            MessageViewModel result = null;
            int successfulInserts = 0;
            int failedInserts = 0;

            // Validate input list
            if (commonViewModel == null || !commonViewModel.Any())
            {
                result= new MessageViewModel
                {
                    Status = "success",
                    Message = "اتصال با موفقیت انجام شد"
                };

                return result;
            }

            foreach (var item in commonViewModel)
            {
                if (item.data_type == "GoodsGrp")
                {
                    // Validate GoodCode
                    if (string.IsNullOrWhiteSpace(item.GoodsGrp_GroupCode) || !long.TryParse(item.GoodsGrp_GroupCode, out long goodCodeParsed) || goodCodeParsed <= 0)
                    {
                        failedInserts++;
                        continue; // Skip invalid entries
                    }

                    try
                    {
                        // Prepare parameters for the stored procedure
                        var parameters = new DynamicParameters();
                        parameters.Add("@action", item.Action);
                        parameters.Add("@groupCode", item.GoodsGrp_GroupCode);
                        parameters.Add("@l1", item.GoodsGrp_L1);
                        parameters.Add("@l2", item.GoodsGrp_L2);
                        parameters.Add("@l3", item.GoodsGrp_L3);
                        parameters.Add("@l4", item.GoodsGrp_L4);
                        parameters.Add("@l5", item.GoodsGrp_L5);
                        parameters.Add("@groupName", item.GoodsGrp_Name);

                        // Call the stored procedure
                        var rowsAffected = _dapper.CallProcedure<int>("sp_editGroup", parameters);

                        // Check the result
                        if (rowsAffected != null && rowsAffected.Any() && rowsAffected[0] > 0)
                        {
                            successfulInserts++;
                        }
                        else
                        {
                            failedInserts++;
                        }
                    }
                    catch (Exception ex)
                    {
                        // Log the exception (consider using a logging framework like Serilog or NLog)
                        Console.WriteLine($"Error inserting Group: {ex.Message}");
                        failedInserts++;
                    }
                }
                else if (item.data_type == "Customer")
                {
                    // Validate GoodCode
                    if (string.IsNullOrWhiteSpace(item.Customer_CustomerCode) || !long.TryParse(item.Customer_CustomerCode, out long goodCodeParsed) || goodCodeParsed <= 0)
                    {
                        failedInserts++;
                        continue; // Skip invalid entries
                    }

                    try
                    {
                        // Prepare parameters for the stored procedure
                        var parameters = new DynamicParameters();
                        parameters.Add("@action", item.Action);
                        parameters.Add("@customerCode", item.Customer_CustomerCode);
                        parameters.Add("@addressRef", item.Customer_AddressRef);
                        parameters.Add("@centralRef", item.Customer_CentralRef);
                        parameters.Add("@cityID", item.Customer_CityId);
                        parameters.Add("@provinceID", item.Customer_ProvinceId);
                        parameters.Add("@priceTip", item.Customer_PriceTip);
                        parameters.Add("@address", item.Customer_Address);
                        parameters.Add("@phone", item.Customer_Phone);
                        parameters.Add("@mobile", item.Customer_Mobile);
                        parameters.Add("@email", item.Customer_email);
                        parameters.Add("@firstName", item.Customer_FirstName);
                        parameters.Add("@lastName", item.Customer_LastName);
                        parameters.Add("@postalCode", item.Customer_ZipPostalCode);


                        // Call the stored procedure
                        var rowsAffected = _dapper.CallProcedure<int>("sp_editCustomer", parameters);

                        // Check the result
                        if (rowsAffected != null && rowsAffected.Any() && rowsAffected[0] > 0)
                        {
                            successfulInserts++;
                        }
                        else
                        {
                            failedInserts++;
                        }
                    }
                    catch (Exception ex)
                    {
                        // Log the exception (consider using a logging framework like Serilog or NLog)
                        Console.WriteLine($"Error inserting Group: {ex.Message}");
                        failedInserts++;
                    }
                }
                else 
                {
                    failedInserts++;
                }
            }





                // Determine the result based on the number of successful and failed inserts
                if (successfulInserts > 0)
                {
                    result = new MessageViewModel
                    {
                        Status = "Success",
                        Message = $"تغییرات بدرستی انجام شد. {successfulInserts}  گروه کالا افزوده شد, {failedInserts}  گروه کالا با شکست مواجهه شد."
                    };
                }
                else
                {
                    result = new MessageViewModel
                    {
                        Status = "Error",
                        Message = commonViewModel.Any() ? $"گروه کالایی جهت افزودن یافت نشد. {failedInserts}  گروه کالا با شکست مواجهه شد." : "لطفا کد گروه کالا معتبر وارد نمایید."
                    };
                }

               // return result;
            

            return result;

        }


    }
}

