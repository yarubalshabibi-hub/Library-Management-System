
namespace trying_about_Library_Management_System
    {
        internal class Program
        {
            static string memberName = "";
            static string memberId = "";
            static string memberEmail = "";
            static string membershipExpiry = "";
            static string memberTier = "";
            static bool isMemberRegistered = false;

            static string bookTitle = "";
            static string bookAuthor = "";
            static string bookGenre = "";
            static int bookCopies = 0;
            static bool isBookRegistered = false;

            static int totalBorrowed = 0;
            static double totalFinesPaid = 0.0;

            static void Main(string[] args)
            {
                int choice = -1;

                while (choice != 15)
                {
                    PrintMenu();
                    Console.Write("Enter your choice: ");
                    string input = Console.ReadLine();

                    if (!int.TryParse(input, out choice))
                    {
                        Console.WriteLine("Please enter a valid number");
                        continue;
                    }

                    switch (choice)
                    {
                        case 0:
                            if (isMemberRegistered)
                                Console.WriteLine("A member is already registered");
                            else
                                RegisterMember();
                            break;

                        case 1:
                            if (!isMemberRegistered)
                                Console.WriteLine("No member registered yet.");
                            else
                                DisplayMemberProfile();
                            break;

                        case 2:
                            if (!isBookRegistered)
                                Console.WriteLine("No book registered yet");
                            else
                            {
                                Console.Write("Enter keyword to search: ");
                                string keyword = Console.ReadLine();
                                bool found = SearchBookByTitle(keyword);
                                Console.WriteLine(found ? "Book found!" : "No book matched your search");
                            }
                            break;

                        case 3:
                            if (!isMemberRegistered || !isBookRegistered)
                                Console.WriteLine("Please register a member and a book first");
                            else
                                BorrowBook(ref bookCopies);
                            break;

                        case 4:
                            if (!isMemberRegistered || !isBookRegistered)
                                Console.WriteLine("Please register a member and a book first");
                            else
                                ReturnBook(ref bookCopies);
                            break;

                        case 5:
                            Console.Write("Enter number of overdue days: ");
                            if (int.TryParse(Console.ReadLine(), out int days) && days >= 0)
                            {
                                double fine = CalculateLateFine(days);
                                totalFinesPaid += fine;
                                Console.WriteLine("Fine amount: " + fine.ToString("F2") + "OMR");
                            }
                            else
                                Console.WriteLine("Invalid number of days");
                            break;

                        case 6:
                            Console.Write("Enter amount: ");
                            if (double.TryParse(Console.ReadLine(), out double amount))
                            {
                                double d1 = ApplyDiscount(amount);
                                double d2 = ApplyDiscount(amount, memberTier);
                                Console.WriteLine("Standard discount (10%): " + d1.ToString("F2") + "OMR");
                                Console.WriteLine("Tier-based discount (" + memberTier + "): " + d2.ToString("F2") + "OMR");
                            }
                            else
                                Console.WriteLine("Invalid amount.");
                            break;

                        case 7:
                            if (!isMemberRegistered)
                                Console.WriteLine("No member registered.");
                            else
                            {
                                bool eligible = CheckBorrowingEligibility(membershipExpiry);
                                Console.WriteLine(eligible
                                    ? "Member is eligible to borrow"
                                    : "Membership has expired. Not eligible");
                            }
                            break;

                        case 8:// Book registration with optional genre
                        if (isBookRegistered)
                                Console.WriteLine("A book is already registered");
                            else
                            {
                                Console.Write("Enter book title: ");
                                string Title = Console.ReadLine();
                                Console.Write("Enter book author: ");
                                string Author = Console.ReadLine();
                                Console.Write("Enter number of copies: ");
                                int.TryParse(Console.ReadLine(), out int Copies);
                                Console.Write("Enter genre (press Enter to skip, default = General): ");
                                string Genre = Console.ReadLine();

                                if (Genre.Trim() == "")
                                    RegisterBook(Title, Author, Copies);
                                else
                                    RegisterBook(Title, Author, Copies, Genre);
                            }
                            break;

                        case 9:
                            if (!isMemberRegistered)
                                Console.WriteLine("Please register a member first");
                            else
                            {
                                memberId = GenerateMemberId();
                                Console.WriteLine("Generated Member ID: " + memberId);
                            }
                            break;

                        case 10:
                            if (!isBookRegistered)
                                Console.WriteLine("No book registered");
                            else
                                DisplayBookDetails(title: bookTitle,
                                                   author: bookAuthor,
                                                   genre: bookGenre,
                                                   copies: bookCopies);
                            break;

                        case 11:
                            Console.Write("Enter renewal days: ");
                            if (int.TryParse(Console.ReadLine(), out int rDays) && rDays > 0)
                            {
                                double fee1 = CalculateRenewalFee(rDays);
                                double fee2 = CalculateRenewalFee(rDays, true);
                                Console.WriteLine("Standard renewal fee: " + fee1.ToString("F2") + "OMR");
                                Console.WriteLine("Premium renewal fee: " + fee2.ToString("F2") + "OMR");
                            }
                            else
                                Console.WriteLine("Invalid number of days");
                            break;

                        case 12:
                            if (isMemberRegistered == true)     // !isMemberRegistered
                            Console.WriteLine("No member registered");
                            else
                            {
                                Console.Write("Enter new email address: ");
                                string newEmail = Console.ReadLine();
                                bool updated = UpdateMemberEmail(newEmail, out string cleanEmail);
                                if (updated)
                                {
                                    memberEmail = cleanEmail;
                                    Console.WriteLine("Email updated to: " + memberEmail);
                                }
                                else
                                    Console.WriteLine("Invalid email Must contain '@' and be at least 6 characters");
                            }
                            break;

                        case 13:
                            PrintSessionSummary();
                            break;

                        case 14:
                            if (!isMemberRegistered)
                                Console.WriteLine("No member registered");
                            else
                            {
                                PrintMemberStatistics();
                                PrintMemberStatistics(true);
                            }
                            break;

                        case 15:
                            Console.WriteLine("Goodbye! Thank you for using the City Public Library");
                            break;

                        default:
                            Console.WriteLine("Invalid choice Please select a number from the menu");
                            break;
                    }

                    Console.WriteLine();
                }
            }
            static void PrintMenu()
            {
                Console.WriteLine("City Public Library System");
                Console.WriteLine("0) Register Member");
                Console.WriteLine("1) Display Member Profile");
                Console.WriteLine("2) Search Book by Title");
                Console.WriteLine("3) Borrow a Book");
                Console.WriteLine("4) Return a Book");
                Console.WriteLine("5) Calculate Late Fine");
                Console.WriteLine("6) Apply Member Discount");
                Console.WriteLine("7) Check Borrowing Eligibility");
                Console.WriteLine("8) Register Book");
                Console.WriteLine("9) Generate Member ID");
                Console.WriteLine("10) Display Book Details");
                Console.WriteLine("11) Calculate Renewal Fee");
                Console.WriteLine("12) Update Member Email");
                Console.WriteLine("13) Session Summary");
                Console.WriteLine("14) Member Statistics (Bonus)");
                Console.WriteLine("15) Exit");
                Console.WriteLine("");
            }

            static void RegisterMember()
            {
                Console.Write("Enter member name: ");
                memberName = Console.ReadLine();

                Console.Write("Enter member email: ");
                memberEmail = Console.ReadLine();

                Console.Write("Enter membership expiry date (yyyy-MM-dd): ");
                membershipExpiry = Console.ReadLine();

                Console.Write("Enter member tier (Standard / Premium): ");
                memberTier = Console.ReadLine();

                int prefixLen = Math.Min(3, memberName.Length);

            if (prefixLen < 3)
            {
                Console.WriteLine("Invalid User Name");
            }
            else
            {

            }
                string prefix = memberName.Substring(0, prefixLen).ToUpper();

                string year = DateTime.Now.Year.ToString();
                memberId = prefix + "-" + year + "-TEMP"; // AHM-2026-TEMP      |       AIH-2026-TEMP       |       YAR-2026-TEMP

                isMemberRegistered = true;
                Console.WriteLine("Member registered! Temporary ID: " + memberId);
                Console.WriteLine("Use option 9 to generate the final unique ID.");
            }
            static void DisplayMemberProfile()
            {
                Console.WriteLine("--- Member Profile ---");
                Console.WriteLine("Name: " + memberName.PadLeft(25));
                Console.WriteLine("ID: " + Convert.ToString(memberId).PadLeft(25));
                Console.WriteLine("Email: " + memberEmail.PadLeft(25));
                Console.WriteLine("Expiry: " + membershipExpiry.PadLeft(25));
                Console.WriteLine("Tier: " + memberTier.PadLeft(25));
               
            }

            static bool SearchBookByTitle(string keyword)
            {
                string lowerTitle = bookTitle.ToLower();
                string lowerKeyword = keyword.ToLower();

                if (lowerKeyword.Length > lowerTitle.Length)
                    return false;

                for (int i = 0; i <= lowerTitle.Length - lowerKeyword.Length; i++)
                {
                    string chunk = lowerTitle.Substring(i, lowerKeyword.Length);
                    if (chunk == lowerKeyword)
                        return true;
                }
                return false;
            }
            static void BorrowBook(ref int copies)
            {
                if (copies <= 0)
                {
                    Console.WriteLine("Sorry, no copies are available to borrow right now.");
                    return;
                }
                copies = Math.Max(0, copies - 1);
                totalBorrowed++;
                Console.WriteLine("Book borrowed successfully! Copies remaining: " + copies);
            }
            static void ReturnBook(ref int copies)
            {
                copies = Math.Min(copies + 1, 9);
                Console.WriteLine("Book returned successfully. Copies now available: " + copies);
            }
            static double CalculateLateFine(int days)
            {
                double fine = days * 0.5 + Math.Sqrt(days) * 0.3;
                return Math.Round(fine, 2);
            }
            static double ApplyDiscount(double amount)
            {
                double result = amount * 0.90;
                return Math.Round(result, 2);
            }

            static double ApplyDiscount(double amount, string tier)
            {
                double rate = 0.90;
                string upperTier = tier.ToUpper();

                if (upperTier == "PREMIUM") rate = 0.75;
                else if (upperTier == "STANDARD") rate = 0.85;

                return Math.Round(amount * rate, 2);
            }

            static bool CheckBorrowingEligibility(string expiryDate)
            {
                DateTime expiry = DateTime.Parse(expiryDate);
                return expiry >= DateTime.Today;
            }

            static void RegisterBook(string title, string author, int copies, string genre = "General")
            {
                bookTitle = title.Trim();
                bookAuthor = author.Trim();
                bookCopies = copies;

                bookGenre = genre.Trim().Length > 0 ? genre.Trim() : "General";

                isBookRegistered = true;
                Console.WriteLine("Book registered! Title: " + bookTitle + " | Genre: " + bookGenre);
            }
           
       static void CalculateRenewalFee()
        {

        }
                }
            }

          
          


    
