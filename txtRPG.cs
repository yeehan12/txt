using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        Console.WriteLine("스파르타 마을에 오신 여러분 환영합니다.");
        Console.WriteLine("원하시는 이름을 입력 해주세요.");
        string name = Console.ReadLine();

        Console.WriteLine("{0}님 환영합니다.", name);
        Console.WriteLine("직업을 선택 해주세요.");
        Console.WriteLine("1. 전사");
        Console.WriteLine("2. 마법사");
        Console.WriteLine("3. 도적");
        Console.WriteLine("4. 궁수");

        string clas = "";
        bool validInput = false;

        while (!validInput)
        {
            int Class = int.Parse(Console.ReadLine());
            switch (Class)
            {
                case 1:
                    clas = "전사";
                    validInput = true;
                    break;
                case 2:
                    clas = "마법사";
                    validInput = true;
                    break;
                case 3:
                    clas = "도적";
                    validInput = true;
                    break;
                case 4:
                    clas = "궁수";
                    validInput = true;
                    break;
                default:
                    Console.WriteLine("잘못 된 입력입니다. 다시 입력 해주세요.");
                    break;
            }
        }

        List<string> myitems = new List<string>
        {
            "무쇠갑옷 | 방어력 +5 | 무쇠로 만들어져 튼튼한 갑옷",
            "스파르타의 창 | 공격력 +7 | 스파르타 전사들이 사용했다는 창",
            "낡은 검 | 공격력 +2 | 쉽게 볼 수 있는 낡은 검",
        };

        string[] shopitems =
        {
            "수련자 갑옷 | 방어력 +5 | 수련에 도움을 주는 갑옷",
            "무쇠갑옷 | 방어력 +5 | 무쇠로 만들어져 튼튼한 갑옷",
            "스파르타의 갑옷 | 방어력 +15 | 스파르타의 전사들이 사용했다는 전설의 갑옷",
            "낡은 검 | 공격력 +2 | 쉽게 볼 수 있는 낡은 검",
            "청동 도끼 | 공격력 +5 | 어디선가 사용 됐던 것 같은 도끼",
            "스파르타의 창 | 공격력 +7 | 스파르타 전사들이 사용했다는 창"
        };

        int[] attackPower = { 0, 7, 2 };
        int[] defensePower = { 5, 0, 0 };
        int[] shopAttackPower = { 0, 0, 0, 2, 5, 7 };
        int[] shopDefensePower = { 5, 5, 15, 0, 0, 0 };
        int[] itemPrices = { 1000, 1000, 3500, 600, 1500, 700 };

        bool[] equipped = new bool[myitems.Count];
        bool[] purchased = new bool[shopitems.Length];

        int baseAttack = 10;
        int baseDefense = 5;
        int currentAttack = baseAttack;
        int currentDefense = baseDefense;

        int Gold = 1500;
        bool exit = false;

        while (!exit)
        {
            Console.WriteLine("이곳에서 던전으로 들어가기 전 활동을 할 수 있습니다.");
            Console.WriteLine("1. 상태 보기");
            Console.WriteLine("2. 인벤토리");
            Console.WriteLine("3. 상점");
            Console.WriteLine("0. 게임 종료");
            int menu = int.Parse(Console.ReadLine());

            switch (menu)
            {
                case 1:
                    Console.WriteLine($"이름 : {name}, 직업 : {clas}");
                    Console.WriteLine($"공격력 : {baseAttack + (currentAttack - baseAttack)} (+{currentAttack - baseAttack})");
                    Console.WriteLine($"방어력 : {baseDefense + (currentDefense - baseDefense)} (+{currentDefense - baseDefense})");
                    Console.WriteLine($"Gold : {Gold}G");
                    Console.WriteLine("0. 나가기");
                    Console.ReadLine();
                    break;

                case 2:
                    bool inventoryExit = false;
                    while (!inventoryExit)
                    {
                        Console.WriteLine("인벤토리 - 장착 관리");
                        for (int i = 0; i < myitems.Count; i++)
                        {
                            string status = equipped[i] ? "[E] " : "";
                            Console.WriteLine($"- {i + 1} {status}{myitems[i]}");
                        }
                        Console.WriteLine("0. 나가기");
                        int equip = int.Parse(Console.ReadLine());
                        if (equip == 0)
                        {
                            inventoryExit = true;
                        }
                        else if (equip > 0 && equip <= myitems.Count)
                        {
                            equipped[equip - 1] = !equipped[equip - 1];
                            string itemName = myitems[equip - 1].Split('|')[0].Trim();
                            if (equipped[equip - 1])
                            {
                                currentAttack += attackPower[equip - 1];
                                currentDefense += defensePower[equip - 1];
                                Console.WriteLine($"{itemName}을(를) 장착했습니다.");
                            }
                            else
                            {
                                currentAttack -= attackPower[equip - 1];
                                currentDefense -= defensePower[equip - 1];
                                Console.WriteLine($"{itemName}을(를) 해제했습니다.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("잘못 된 입력입니다. 다시 입력 해주세요.");
                        }
                    }
                    break;

                case 3:
                    bool shopExit = false;
                    while (!shopExit)
                    {
                        Console.WriteLine($"[보유 골드] {Gold}G");
                        for (int i = 0; i < shopitems.Length; i++)
                        {
                            bool alreadyOwned = myitems.Contains(shopitems[i]);
                            string status = purchased[i] || alreadyOwned ? "[구매완료]" : $"{itemPrices[i]}G";
                            Console.WriteLine($"- {i + 1} {shopitems[i]} | {status}");
                        }
                        Console.WriteLine("1. 아이템 구매");
                        Console.WriteLine("0. 나가기");
                        int subMenu = int.Parse(Console.ReadLine());

                        if (subMenu == 1)
                        {
                            Console.WriteLine("구매할 아이템 번호를 입력하세요.");
                            int itemNumber = int.Parse(Console.ReadLine());

                            if (itemNumber > 0 && itemNumber <= shopitems.Length && !purchased[itemNumber - 1])
                            {
                                if (Gold >= itemPrices[itemNumber - 1])
                                {
                                    Gold -= itemPrices[itemNumber - 1];
                                    purchased[itemNumber - 1] = true;
                                    myitems.Add(shopitems[itemNumber - 1]);

                                    Array.Resize(ref attackPower, attackPower.Length + 1);
                                    Array.Resize(ref defensePower, defensePower.Length + 1);
                                    Array.Resize(ref equipped, myitems.Count);

                                    attackPower[attackPower.Length - 1] = shopAttackPower[itemNumber - 1];
                                    defensePower[defensePower.Length - 1] = shopDefensePower[itemNumber - 1];
                                    equipped[equipped.Length - 1] = false;

                                    Console.WriteLine($"{shopitems[itemNumber - 1].Split('|')[0].Trim()}을(를) 구매했습니다.");
                                }
                                else
                                {
                                    Console.WriteLine("골드가 부족합니다.");
                                }
                            }
                        }
                        else if (subMenu == 0)
                        {
                            shopExit = true;
                        }
                        else
                        {
                            Console.WriteLine("잘못 된 입력입니다. 다시 입력 해주세요.");
                        }
                    }
                    break;



                case 0:
                    exit = true;
                    break;

                default:
                    Console.WriteLine("잘못 된 입력입니다. 다시 입력 해주세요.");
                    break;
            }
        }
    }
}
