using System;
using System.Collections.Generic;
using System.Linq;
using LXP2CYD.Settings.Cities;
using LXP2CYD.Settings.Provinces;

namespace LXP2CYD.EntityFrameworkCore.Seed.Host
{
    public class DefaultStaticDataCreator
    {
        private readonly LXP2CYDDbContext _context;
        public DefaultStaticDataCreator(LXP2CYDDbContext context)
        {
            _context = context;
        }
        public void Create()
        {
            CreateProvinces();
        }
        public void CreateProvinces()
        {
            var province = _context.Provinces.FirstOrDefault();
            if (province == null)
            {
                var provinces = new List<Province>
                {
                    new Province
                    {
                        Name = "Eastern Cape",
                        Cities = new List<City>
                        {
                            new City
                            {
                                Name ="Alice"
                            },
                            new City
                            {
                                Name ="Butterworth"
                            },
                            new City
                            {
                                Name ="East London"
                            },
                            new City
                            {
                                Name ="Graaff-Reinet"
                            },
                            new City
                            {
                                Name ="Grahamstown"
                            },
                            new City
                            {
                                Name ="King William’s Town"
                            },
                            new City
                            {
                                Name ="Mthatha"
                            },
                            new City
                            {
                                Name ="Port Elizabeth"
                            },
                            new City
                            {
                                Name ="Queenstown"
                            },
                            new City
                            {
                                Name ="Uitenhage"
                            },
                            new City
                            {
                                Name ="Zwelitsha"
                            }
                        }
                    },
                    new Province
                    {
                        Name = "Western Cape",
                        Cities = new List<City>
                        {
                            new City
                            {
                                Name ="Bellville"
                            },
                            new City
                            {
                                Name ="Cape Town"
                            },
                            new City
                            {
                                Name ="Constantia"
                            },
                            new City
                            {
                                Name ="George"
                            },
                            new City
                            {
                                Name ="Hopefield"
                            },
                            new City
                            {
                                Name ="Oudtshoorn"
                            },
                            new City
                            {
                                Name ="Paarl"
                            },
                            new City
                            {
                                Name ="Simon’s Town"
                            },
                            new City
                            {
                                Name ="Stellenbosch"
                            },
                            new City
                            {
                                Name ="Swellendam"
                            },
                            new City
                            {
                                Name ="Worcester"
                            }
                        }
                    },
                    new Province
                    {
                        Name = "Nothern Cape",
                        Cities = new List<City>
                        {
                            new City
                            {
                                Name ="Kimberley"
                            },
                            new City
                            {
                                Name ="Kuruman"
                            },
                            new City
                            {
                                Name ="Port Nolloth"
                            },

                        }
                    },
                    new Province
                    {
                        Name = "Gauteng",
                        Cities = new List<City>
                        {
                            new City
                            {
                                Name ="Benoni"
                            },
                            new City
                            {
                                Name ="Boksburg"
                            },
                            new City
                            {
                                Name ="Brakpan"
                            },
                            new City
                            {
                                Name ="Carletonville"
                            },
                            new City
                            {
                                Name ="Germiston"
                            },
                            new City
                            {
                                Name ="Johannesburg"
                            },
                            new City
                            {
                                Name ="Krugersdorp"
                            },
                            new City
                            {
                                Name ="Pretoria"
                            },
                            new City
                            {
                                Name ="Randburg"
                            },
                            new City
                            {
                                Name ="Randfontein"
                            },
                            new City
                            {
                                Name ="Roodepoort"
                            },
                            new City
                            {
                                Name ="Soweto"
                            },
                            new City
                            {
                                Name ="Springs"
                            },
                            new City
                            {
                                Name ="Vanderbijlpark"
                            },
                            new City
                            {
                                Name ="Vereeniging"
                            }
                        }
                    },
                    new Province
                    {
                        Name = "Free State",
                         Cities = new List<City>
                        {
                            new City
                            {
                                Name ="Bethlehem"
                            },
                            new City
                            {
                                Name ="Bloemfontein"
                            },
                            new City
                            {
                                Name ="Jagersfontein"
                            },
                            new City
                            {
                                Name ="Kroonstad"
                            },
                            new City
                            {
                                Name ="Odendaalsrus"
                            },
                            new City
                            {
                                Name ="Parys"
                            },
                            new City
                            {
                                Name ="Phuthaditjhaba"
                            },
                            new City
                            {
                                Name ="Sasolburg"
                            },
                            new City
                            {
                                Name ="Virginia"
                            },
                            new City
                            {
                                Name ="Welkom"
                            }
                        }
                    },
                    new Province
                    {
                        Name = "Limpompo",
                        Cities = new List<City>
                        {
                            new City
                            {
                                Name ="Giyani"
                            },
                            new City
                            {
                                Name ="Lebowakgomo"
                            },
                            new City
                            {
                                Name ="Musina"
                            },
                            new City
                            {
                                Name ="Phalaborwa"
                            },
                            new City
                            {
                                Name ="Polokwane"
                            },
                            new City
                            {
                                Name ="Seshego"
                            },
                            new City
                            {
                                Name ="Sibasa"
                            },
                            new City
                            {
                                Name ="Thabazimbi"
                            }

                        }
                    },
                    new Province
                    {
                        Name = "Mpumalanga",
                        Cities = new List<City>
                        {
                            new City
                            {
                                Name ="Emalahleni"
                            },
                            new City
                            {
                                Name ="Nelspruit"
                            },
                            new City
                            {
                                Name ="Secunda"
                            },
                           
                        }
                    },
                    new Province
                    {
                        Name = "North West",
                        Cities = new List<City>
                        {
                            new City
                            {
                                Name ="Klerksdorp"
                            },
                            new City
                            {
                                Name ="Mahikeng"
                            },
                            new City
                            {
                                Name ="Mmabatho"
                            },
                            new City
                            {
                                Name ="Potchefstroom"
                            },
                            new City
                            {
                                Name ="Rustenburg"
                            },
                          
                        }
                    },
                    new Province
                    {
                        Name = "KwaZulu Natal",
                        Cities = new List<City>
                        {
                            new City
                            {
                                Name ="Durban"
                            },
                            new City
                            {
                                Name ="Empangeni"
                            },
                            new City
                            {
                                Name ="Ladysmith"
                            },
                            new City
                            {
                                Name ="Newcastle"
                            },
                            new City
                            {
                                Name ="Pietermaritzburg"
                            },
                            new City
                            {
                                Name ="Pinetown"
                            },
                            new City
                            {
                                Name ="Ulundi"
                            },
                            new City
                            {
                                Name ="Umlazi"
                            }
                        }
                    }
                };
                _context.Provinces.AddRange(provinces);
                _context.SaveChanges();
            }
        }
    }
}
