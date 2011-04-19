namespace Web.Helper
{
    using System;

    public class NameGenerator
    {
        public string[] strGiven = new string[] { 
            "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", 
            "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z"
         };
        public string[] strSur = new string[] { 
            "Alexander", "Allen", "Alston", "Andersen", "Anthony", "Arenas", "Arroyo", "Artest", "Atkins", "Augmon", "Baker", "Banks", "Barbosa", "Barnes", "Barry", "Battie", 
            "Battier", "Baxter", "Beasley", "Bell", "Bender", "Best", "Bibby", "Billups", "Blake", "Blount", "Bogans", "Booth", "Boozer", "Bosh", "Bowen", "Boykins", 
            "Bradley", "Brand", "Bremer", "Brewer", "Brezec", "Brown", "Bryant", "Buckner", "Buford", "Butler", "Camby", "Campbell", "Cardinal", "Carroll", "Carter", "Cassell", 
            "Cato", "Chandler", "Cheaney", "Christie", "Clark", "Claxton", "Coleman", "Coles", "Collier", "Collins", "Collison", "Cook", "Crawford", "Croshere", "Curry", "Dalembert", 
            "Dampier", "Daniels", "Davis", "Delk", "Diaw", "Dickau", "Diop", "Divac", "Dixon", "Doleac", "Dooling", "Drobnjak", "Duncan", "Dunleavy", "Dupree", "Ebi", 
            "Eisley", "Elson", "Ely", "Evans", "Finley", "Fisher", "Fizer", "Ford", "Fortson", "Foster", "Fowlkes", "Fox", "Foyle", "Frahm", "Francis", "Gadzuric", 
            "Gaines", "Garcia", "Garnett", "Garrity", "Gasol", "George", "Gill", "Ginobili", "Giricek", "Glover", "Gooden", "Grant", "Green", "Griffin", "Haislip", "Ham", 
            "Hamilton", "Hansen", "Hardaway", "Harpring", "Harris", "Hart", "Harvey", "Haslem", "Hassell", "Hayes", "Haywood", "Henderson", "Hill", "Hinrich", "Hoiberg", "Horry", 
            "House", "Houston", "Howard", "Hudson", "Hughes", "Hunter", "Iverson", "Jackson", "Jacobsen", "James", "Jamison", "Jaric", "Jefferies", "Jefferson", "Jeffries", "Johnsen", 
            "Johnson", "Jones", "Kaman", "Kapono", "Kidd", "Kittles", "Knight", "Korver", "Kukoc", "Laettner", "LaFrentz", "Lampe", "Lenard", "Lewis", "Lopez", "Lue", 
            "Lynch", "Madsen", "Maggette", "Magloire", "Malone", "Marbury", "Marion", "Marks", "Marshall", "Martin", "Mashburn", "Mason", "McCarty", "McCaskill", "McDyess", "McGrady", 
            "McInnis", "McKie", "Mihm", "Miles", "Milicic", "Miller", "Mobley", "Mohammed", "Moiso", "Moore", "Mourning", "Murphy", "Murray", "Mutombo", "Nachbar", "Nailon", 
            "Najera", "Nash", "N＇diaye", "Nene", "Newble", "Norris", "Odom", "Okur", "Ollie", "O＇Neal", "Ostertag", "Outlaw", "Overton", "Padgett", "Palacio", "Pargo", 
            "Parker", "Patterson", "Payton", "Peeler", "Perkins", "Person", "Peterson", "Pierce", "Pietrus", "Pippen", "Pollard", "Pope", "Posey", "Prince", "Ratliff", "Rebraca", 
            "Redd", "Richardson", "Ridnour", "Robinson", "Rogers", "Rooks", "Rose", "Ruffin", "Rush", "Russell", "Salmons", "Sampson", "Santiago", "Sesay", "Shirley", "Skinner", 
            "Slay", "Smith", "Snow", "Sprewell", "Stevenson", "Stewart", "Sura", "Sweetney", "Swift", "Taylor", "Terry", "Thomas", "Tinsley", "Traylor", "Trent", "Wade", 
            "Wagner", "Walker", "Wallace", "Walton", "Ward", "Watson", "Webber", "Wells", "Welsch", "Wesley", "Woods", "West", "White", "Whitney", "Wilcox", "Wilks", 
            "Willis", "Wright"
         };

        public string GetName()
        {
            int index = RandomItem.rnd.Next(0, 0x111);
            int num2 = RandomItem.rnd.Next(0, 0x19);
            return (this.strGiven[num2] + "." + this.strSur[index]);
        }
    }
}

