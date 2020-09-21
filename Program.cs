using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace OkeyDemo
{
    class Program
    {
        static string[] colors = new string[] { "Yellow", "Blue", "Black", "Red" };
        static string[] symbols = new string[] { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13" };

        static void Main(string[] args)
        {
            List<Stone> player1 = new List<Stone>();
            List<Stone> player2 = new List<Stone>();
            List<Stone> player3 = new List<Stone>();
            List<Stone> player4 = new List<Stone>();

            List<Stone> allstones = new List<Stone>();

            Stone indicatorstone, okeystone, fake;

            for (int i = 0; i < colors.Length; i++)
            {
                for (int j = 0; j < symbols.Length; j++)
                {
                    Stone stone = new Stone(colors[i], symbols[j]);
                    allstones.Add(stone);
                    allstones.Add(stone);
                }
            }

            Random rnd = new Random();
            int indicatorindex = rnd.Next(104);    
            indicatorstone = allstones[indicatorindex];
            allstones.RemoveAt(indicatorindex);
            Console.WriteLine(allstones.Count);
            Console.WriteLine(indicatorstone.ToString());
                       
            if (indicatorstone.getValue() == 13)
            {
                okeystone = new Stone(indicatorstone.getColor(), "1");
                setOkey(okeystone);
            }
            else
            {
                okeystone = new Stone(indicatorstone.getColor(), (indicatorstone.getValue() + 1).ToString());
                setOkey(okeystone);
            }             

            Console.WriteLine(okeystone.ToString());
            fake = new Stone("Black", "Fake");
            fake.setValue(okeystone.getValue());
            allstones.Add(fake);
            allstones.Add(fake);
            Console.WriteLine(fake.ToString() + " " + fake.getValue());
            Console.WriteLine(allstones.Count);

            for (int i = 105; i > 49; i--)
            {
                int s = rnd.Next(i);
                if ((i - 1) % 4 == 0)
                {
                    player1.Add(allstones[s]);
                    allstones.RemoveAt(s);
                }
                else if ((i - 1) % 4 == 1)
                {
                    player2.Add(allstones[s]);
                    allstones.RemoveAt(s);
                }
                else if ((i - 1) % 4 == 2)
                {
                    player3.Add(allstones[s]);
                    allstones.RemoveAt(s);
                }
                else
                {
                    player4.Add(allstones[s]);
                    allstones.RemoveAt(s);
                }
            }

            player1.Sort(delegate (Stone s1, Stone s2) { return s1.getValue().CompareTo(s2.getValue()); });
            player2.Sort(delegate (Stone s1, Stone s2) { return s1.getValue().CompareTo(s2.getValue()); });
            player3.Sort(delegate (Stone s1, Stone s2) { return s1.getValue().CompareTo(s2.getValue()); });
            player4.Sort(delegate (Stone s1, Stone s2) { return s1.getValue().CompareTo(s2.getValue()); });

            PrintPlayerStones(player1);
            PrintPlayerStones(player2);
            PrintPlayerStones(player3);
            PrintPlayerStones(player4);

            KneeHands(player1);
            KneeHands(player2);
            KneeHands(player3);
            KneeHands(player4);                                                       

            Console.ReadKey();

            void PrintPlayerStones(List<Stone> player)
            {
                Console.WriteLine();
                Console.Write("Player: ");
                for (int i = 0; i < player.Count; i++)
                {
                    Console.Write(player[i].ToString() + " ");
                }
                Console.WriteLine();
            }

            void setOkey(Stone stone)
            {
                for(int i = 0; i < allstones.Count; i++)
                {
                    if(allstones[i].getColor().Equals(stone.getColor()) && allstones[i].getSymbol().Equals(stone.getSymbol()))
                    {
                        allstones[i].setOkey(true);
                    }
                }
            }

            void KneeHands(List<Stone> playerStones)
            {
                int point = 0;
                int okeyCount = playerStones.Where(s => s.isOkey()).Count();

                //Combine stones of the same color    
                List<Stone> yellowstones = playerStones.Where(s => s.getColor().Equals("Yellow")).ToList();  
                for (int i = yellowstones.Count(); i>=3; i--)
                {
                    for(int j = 0; j < yellowstones.Count() - i; j++)
                    {
                        if (hasAscendingGroup(j, j + i - 1, yellowstones))
                        {
                            Console.WriteLine("Yellows have " + i + " stones group");
                        };
                    }
                }                
                List<Stone> bluestones = playerStones.Where(s => s.getColor().Equals("Blue")).ToList();
                for (int i = bluestones.Count(); i >= 3; i--)
                {
                    for (int j = 0; j < bluestones.Count() - i; j++)
                    {
                        if (hasAscendingGroup(j, j + i - 1, bluestones))
                        {
                            Console.WriteLine("Blues have " + i + " stones group");
                        };
                    }
                }
                List<Stone> blackstones = playerStones.Where(s => s.getColor().Equals("Black")).ToList();
                for (int i = blackstones.Count(); i >= 3; i--)
                {
                    for (int j = 0; j < blackstones.Count() - i; j++)
                    {
                        if (hasAscendingGroup(j, j + i - 1, blackstones))
                        {
                            Console.WriteLine("Blacks have " + i + " stones group");
                        };
                    }
                }
                List<Stone> redstones = playerStones.Where(s => s.getColor().Equals("Red")).ToList();
                for (int i = redstones.Count(); i >= 3; i--)
                {
                    for (int j = 0; j < redstones.Count() - i; j++)
                    {
                        if (hasAscendingGroup(j, j + i - 1, redstones))
                        {
                            Console.WriteLine("Reds have " + i + " stones group");
                        };
                    }
                }

            }


            int getDifferentColoredNeighborCount(Stone stone, List<Stone> playerStones)
            {
                int x=0;
                bool yellow = false, blue = false, black = false, red = false;
                for(int i = 0; i < playerStones.Count; i++)
                {
                    if (!stone.getColor().Equals(playerStones[i].getColor()) && stone.getSymbol().Equals(playerStones[i].getSymbol()))
                    {
                        if (playerStones[i].getColor().Equals("Yellow") && !yellow)
                        {
                            x++;
                            yellow = true;
                        }
                        else if (playerStones[i].getColor().Equals("Blue") && !blue)
                        {
                            x++;
                            blue = true;
                        }
                        else if (playerStones[i].getColor().Equals("Black") && !black)
                        {
                            x++;
                            black = true;
                        }
                        else if (playerStones[i].getColor().Equals("Red") && !red)
                        {
                            x++;
                            red = true;
                        }
                    }
                }
                return x;
            }

            bool hasOneMoreNeighbor(Stone stone, List<Stone> playerStones)
            {
                bool found = false;
                for (int i = 0; i < playerStones.Count; i++)
                {
                    if(playerStones[i].getColor().Equals(stone.getColor()) && playerStones[i].getValue().Equals(stone.getValue() + 1))
                    {
                        found = true;
                    }
                }
                return found;
            }

            bool hasAscendingGroup(int start, int end, List<Stone> stones)
            {
                bool group = true;
                for(int i = start; i < end; i++)
                {
                    if(!hasOneMoreNeighbor(stones[i], stones))
                    {
                        group = false;
                    }
                }
                return group;
            }
        }
    } }
