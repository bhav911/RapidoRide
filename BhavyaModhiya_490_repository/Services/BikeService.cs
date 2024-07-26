using BhavyaModhiya_490_Model.Context;
using BhavyaModhiya_490_Model.CustomModel;
using BhavyaModhiya_490_Models.SqlHelper;
using BhavyaModhiya_490_repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BhavyaModhiya_490_repository.Services
{
    public class BikeService : IBikeInterface
    {
        private readonly BhavyaModhiya_490Entities _db = new BhavyaModhiya_490Entities();

        public List<Bike> GetAllBikes() 
        {
            try
            {
                List<Bike> bikeList = _db.Bike.Where(q => q.isDeleted == false).ToList();
                return bikeList;
            }
            catch (Exception ex)
            {
                return null;
                throw;
            }
        }


        public List<BikeModel> GetAllBikes(FilterModel filter)
        {
            try
            {
                List<BikeModel> bikeList = new List<BikeModel>();
                string dynamicQuery = "isDeleted = 0 AND ";
                if (!filter.kmDriven.Equals("*") && !filter.yearsOfUsage.Equals("*"))
                {
                    string[] km = filter.kmDriven.Split(',');
                    string[] you = filter.yearsOfUsage.Split(',');
                    int skm = Convert.ToInt32(km[0]);
                    int ekm = Convert.ToInt32(km[1]);
                    int syou = Convert.ToInt16(you[0]);
                    int eyou = Convert.ToInt16(you[1]);

                    if (!filter.brand.Equals("*"))
                    {
                        foreach (string brand in filter.brand.Split(','))
                        {
                            dynamicQuery += $"Brand = '{brand}' AND ";
                        }
                    }
                    
                    dynamicQuery += $"kilometresDriven >= {skm} AND kilometresDriven <= {ekm} AND ";
                    dynamicQuery += $"Years >= {syou} AND Years <= {eyou}";          

                    
                }
                Dictionary<string, object> parameters = new Dictionary<string, object>()
                {
                    {"@DynamicConditions", dynamicQuery },
                    {"@page", filter.fetchPage}
                };
                DataTable result = SqlSPHelper.SqlStoredProcedure("GetFilteredBikes", parameters);
                foreach (DataRow row in result.Rows)
                {
                    BikeModel bike = new BikeModel()
                    {
                        BikeID = Convert.ToInt32(row["BikeID"]),
                        Brand = Convert.ToString(row["Brand"]),
                        Models = Convert.ToString(row["Models"]),
                        Description = Convert.ToString(row["Description"]),
                        KilometresDriven = Convert.ToInt32(row["kilometresDriven"]),
                        Price = Convert.ToInt32(row["Price"]),
                        SellerID = Convert.ToInt32(row["SellerID"]),
                        Years = Convert.ToByte(row["Years"]),
                        FirstName = Convert.ToString(row["FirstName"]),
                        LastName = Convert.ToString(row["LastName"]),
                        Email = Convert.ToString(row["Email"]),
                        PhoneNumber = Convert.ToString(row["PhoneNumber"]),
                        Image = Convert.ToString(row["Image"])
                    };
                    bikeList.Add(bike);
                }
                return bikeList;
            }
            catch (Exception ex)
            {
                return null;
                throw;
            }
        }

        public Bike GetBike(int bikeID)
        {
            try
            {
                Bike bike = _db.Bike.FirstOrDefault(q => q.BikeID == bikeID && q.isDeleted == false);
                return bike;
            }
            catch (Exception ex)
            {
                return null;
                throw;
            }
        }
        public Seller AddSeller(RegisterModel registerModel)
        {
            try
            {
                Seller seller = new Seller()
                {
                    FirstName = registerModel.FirstName,
                    LastName = registerModel.LastName,
                    Email = registerModel.Email,
                    PhoneNumber = registerModel.PhoneNumber
                };

                seller = _db.Seller.Add(seller);
                _db.SaveChanges();
                return seller;
            }
            catch (Exception ex)
            {
                return null;
                throw;
            }
        }
        public bool AddBike(RegisterModel registerModel)
        {
            try
            {
                Bike bike = new Bike()
                {
                    Brand = registerModel.Brand,
                    Models = registerModel.Models,
                    kilometresDriven = registerModel.kilometresDriven,
                    Price = registerModel.Price,
                    SellerID = registerModel.SellerID,
                    Years = registerModel.Years,
                    Description = registerModel.Description,
                    Image = registerModel.Image,
                    isDeleted = false
                };

                _db.Bike.Add(bike);
                _db.SaveChanges();
                return true;
            }
            catch (Exception ex) 
            {
                return false;
                throw;
            }
        }

        public bool UpdateRegistery(RegisterModel registerModel)
        {
            try
            {
                Bike bike = _db.Bike.FirstOrDefault(q => q.BikeID == registerModel.BikeID);
                Seller seller = bike.Seller;
                seller.FirstName = registerModel.FirstName;
                seller.LastName = registerModel.LastName;
                seller.Email = registerModel.Email;
                seller.PhoneNumber = registerModel.PhoneNumber;
                bike.Brand = registerModel.Brand;
                bike.Models = registerModel.Models;
                bike.kilometresDriven = registerModel.kilometresDriven;
                bike.Price = registerModel.Price;
                bike.Years = registerModel.Years;
                bike.Image = registerModel.Image;
                bike.Description = registerModel.Description;
                _db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
                throw;
            }
        }

        public bool DeleteBike(int bikeID)
        {
            try
            {
                Bike bike = _db.Bike.FirstOrDefault(q => q.BikeID == bikeID);
                bike.isDeleted = true;
                _db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
                throw;
            }
        }

        public string GetMyFavorites(int userID)
        {
            try
            {
                string bikeIDs = _db.FAVORITES.FirstOrDefault(q => q.UserID == userID).BikeIDs;
                return bikeIDs;
            }
            catch (Exception)
            {
                return "";
                throw;
            }
        }

        public bool ToggleFavorit(int userID, int bikeID)
        {
            try
            {
                Dictionary<string, object> parameters = new Dictionary<string, object>()
                {
                    {"@UserID", userID },
                    {"@BikeID", bikeID}
                };

                DataTable result = SqlSPHelper.SqlStoredProcedure("ToggleFavorite", parameters);
                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }

        public List<Bike> GetBikesOfIDs(string[] bikeIDs)
        {
            try
            {
                List<Bike> bikeList = new List<Bike>();
                foreach (string st in bikeIDs)
                {
                    if (st != "")
                    {
                        int bikeID = Convert.ToInt32(st);
                        bikeList.Add(_db.Bike.FirstOrDefault(q => q.BikeID == bikeID));
                    }
                }

                return bikeList;
            }
            catch (Exception ex)
            {
                return null;
                throw;
            }
        }
    }
}
