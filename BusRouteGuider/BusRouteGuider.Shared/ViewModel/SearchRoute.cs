﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.IO;
using Windows.Storage;
using System.Threading.Tasks;
using System.Windows;


/*
    This class contains the logic for reading the data file, inerting data into the data structures and searching the route
 */
namespace BusRouteGuider.ViewModel
{
    class SearchRoute
    {
        private static Dictionary<String, Route> routes;            //Stores all the available routes
        private static Dictionary<String, Location> locations;      //Stores all the available locations
        
        //Constructor for the class
        public SearchRoute() {            
            start();
        }

        //Intialize the data structures
        public void start() { 
            routes = new Dictionary<String, Route>();
            locations = new Dictionary<String, Location>();
            loadData();
            Debug.WriteLine("++++++++++++++++++++++++");
        }


        //Loading data from data file
        public async void loadData() {
            Debug.WriteLine("Load Data Came");
            String[] lines;
            String[] temp;
            Route route;

            //Reading the text file
            var folder = Windows.ApplicationModel.Package.Current.InstalledLocation;
            var file = await folder.GetFileAsync("data.txt");
            var contents =  await Windows.Storage.FileIO.ReadTextAsync(file);

            //Seperate into variables
            lines = contents.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.None);

           
            //string line = "001|Fort,Kiribathgoda,Miriswatha,Nittambuwa,Warakapola,Nelundeniya,Galigamuwa,Kegalle,Mawanella,Kadugannawa,Peradeniya,Kandy|Kandy,Peradeniya,Kadugannawa,Mawanella,Kegalle,Galigamuwa,Nelundeniya,Warakapola,Nittambuwa,Miriswatha,Kiribathgoda,Fort  \n 002|Fort,Weligama,Ahangama,Koggala,Galle,Rajgama,Hikkaduwa,Ambalangoda,Kosgoda,Bentota,Beruwala,Kalutara,Wadduwa,Panadura,Moratuwa,Wellawatte,Matara|Matara,Wellawatte,Moratuwa,Panadura,Wadduwa,Kalutara,Beruwala,Bentota,Kosgoda,Ambalangoda,Hikkaduwa,Rajgama,Galle,Koggala,Ahangama,Weligama,Fort   \n 003|Fort,Wellampitiya,Kaduwela,Hanwella,Avissawella,Eheliyagoda,Kuruwitta,Rathnapura,Lellopitiya,Pelmadulla,Kahawatta,Godakawela,Udawalawe,Embilipitiya,Suriyawewa,Nonagama,Weerawila,Katharagama|Katharagama,Weerawila,Nonagama,Suriyawewa,Embilipitiya,Udawalawe,Godakawela,Kahawatta,Pelmadulla,Lellopitiya,Rathnapura,Kuruwitta,Eheliyagoda,Avissawella,Hanwella,Kaduwela,Wellampitiya,Fort  \n 004|Fort,Wattala,Kandana,Ja-Ela Seeduwa,Katunayake,Negombo,Kochikade,Wennapuwa,Marawila,Chilaw,Arachchikattu,Akkaraweli,Amaikuli,Puttalam,Karuwalagaswewa,Kala Oya,Nochiyagama,Anuradhapura,Rambewa,Medawachchiya,Mankulam,Cheddikulam,Madhu Road,Murunkan,Mannar,Pesalai,Talaimannar|Talaimannar,Pesalai,Mannar,Murunkan,Madhu Road,Cheddikulam,Mankulam,Medawachchiya,Rambewa,Anuradhapura,Nochiyagama,Kala Oya,Karuwalagaswewa,Puttalam,Amaikuli,Akkaraweli,Arachchikattu,Chilaw,Marawila,Wennapuwa,Kochikade,Negombo,Katunayake,Ja-Ela Seeduwa,Kandana,Wattala,Fort  \n 005|Fort,Wattala,Ja-Ela,Minuwangoda,Divulapitiya,Giriulla,Narammala,Kurunegala|Kurunegala,Narammala,Giriulla,Divulapitiya,Minuwangoda,Ja-Ela,Wattala,Fort  \n 006|Fort,Kiribathgoda,Miriswatta,Nittambuwa,Warakapola,Ambepussa,Polgahawela|Polgahawela,Ambepussa,Warakapola,Nittambuwa,Miriswatta,Kiribathgoda,Fort   \n 007|Fort,Negambo,Chilaw,Puttalam,Kalpitiya|Kalpitiya,Puttalam,Chilaw,Negambo,Fort   \n 015|Fort,Nittambuwa,Warakapola,Polgahawela,Kurunegala,Dambulla,Kekirawa,Anuradhapura|Anuradhapura,Kekirawa,Dambulla,Kurunegala,Polgahawela,Warakapola,Nittambuwa,Fort   \n 017|Panadura,Dehiwala,Kotagama Sri Vachissasa Mawatha,Kalubowila,Nugegoda,Pagoda Rd,Kotte Rd,Sri Jayawardana Pura Mw,Battaramulla,Kaduwela Rd,Thalahena,Malabe,Kaduwela,Biyagama,New Kandy Road,Waliweriya,Balum Mahara Junction,Fort-Kandy Rd,Pasyala,Nittabuwa,Warakapola,Kegalle,Mawanella,Pilimathalawa,Peradeniya,Kandy|Kandy,Peradeniya,Pilimathalawa,Mawanella,Kegalle,Warakapola,Nittabuwa,Pasyala,Fort-Kandy Rd,Balum Mahara Junction,Waliweriya,New Kandy Road,Biyagama,Kaduwela,Malabe,Thalahena,Kaduwela Rd,Battaramulla,Sri Jayawardana Pura Mw,Kotte Rd,Pagoda Rd,Nugegoda,Kalubowila,Kotagama Sri Vachissasa Mawatha,Dehiwala,Panadura   \n 022|Kandy,Hunnasgiriya,Hasalaka,mahiyanaganaya,Padiyatalawa,Mahaoya,Ampara|Ampara,Mahaoya,Padiyatalawa,mahiyanaganaya,Hasalaka,Hunnasgiriya,Kandy   \n 064|Panadura,Follow A8,Rathnapura,Balangoda,Belihuloya,Beragala,Haputhale,Bandarawela,Demodara,Hali Ela,Badulla,Passara,Lunugala,Bibila,Uraniya,Mahiyanganaya|Mahiyanganaya,Uraniya,Bibila,Lunugala,Passara,Badulla,Hali Ela,Demodara,Bandarawela,Haputhale,Beragala,Belihuloya,Balangoda,Rathnapura,Follow A8,Panadura   \n 087|Ja-Ela,Negombo,Chilaw,Puttlam,Anuradhapura,Vavuniya,Killinochchi,Elehantpass,Jaffna|Jaffna,Elehantpass,Killinochchi,Vavuniya,Anuradhapura,Puttlam,Chilaw,Negombo,Ja-Ela  \n 098|Fort,Kaduwela,Hanwella,Avissawella,Rathnapura,Pelmadulla,Udawalawe National Park,Monaragala|Monaragala,Udawalawe National Park,Pelmadulla,Rathnapura,Avissawella,Hanwella,Kaduwela,Fort   \n 099|Fort,Maharagama,Homagama,Avissawella,Rathnapura,Pelmadulla,Balangoda,Belihuloya,Beragala,Haputhale,Bandarawela,Badulla|Badulla,Bandarawela,Haputhale,Beragala,Belihuloya,Balangoda,Pelmadulla,Rathnapura,Avissawella,Homagama,Maharagama,Fort   \n 100|Panadura,Walana,Old Galle Road,Keselwatta,Moratuwa,Ratmalana,Mt. Lavinia (Galkissa),Dehiwala,Wellawatte,Bambalapitiya,Kollupitiya,Galle Face,Fort,Pettah|Pettah,Fort,Galle Face,Kollupitiya,Bambalapitiya,Wellawatte,Dehiwala,Mt. Lavinia (Galkissa),Ratmalana,Moratuwa,Keselwatta,Old Galle Road,Walana,Panadura   \n 101|Moratuwa,Ratmalana,Mt. Lavinia (Galkissa),Dehiwala,Wellawatte,Bambalapitiya,Kollupitiya,Slave Island,Lake House,Fort,Pettah|Pettah,Fort,Lake House,Slave Island,Kollupitiya,Bambalapitiya,Wellawatte,Dehiwala,Mt. Lavinia (Galkissa),Ratmalana,Moratuwa  \n 102|Moratuwa (Angulana),Ratmalana,Mt. Lavinia (Galkissa),Dehiwala,Wellawatte,Bambalapitiya,Kollupitiya,Galle Face,Fort,Kochchikade,Kotahena|Kotahena,Kochchikade,Fort,Galle Face,Kollupitiya,Bambalapitiya,Wellawatte,Dehiwala,Mt. Lavinia (Galkissa),Ratmalana,Moratuwa (Angulana)   \n 103|Narahenpita,Kanatta Junction,Borella,Punchi Borella,Maradana,Technical College Junction,Pettah,Fort|Fort,Pettah,Technical College Junction,Maradana,Punchi Borella,Borella,Kanatta Junction,Narahenpita  \n 104|Ja-Ela,Wattala,Peliyagoda,New Kelani Bridge,Orugodawatta,Dematagoda,Borella,Kanatta Junction,Bauddhaloka Mawatha,Thummulla,Bambalapitiya|Bambalapitiya,Thummulla,Bauddhaloka Mawatha,Kanatta Junction,Borella,Dematagoda,Orugodawatta,New Kelani Bridge,Peliyagoda,Wattala,Ja-Ela  \n 106|Elakanda,Wattala,Peliyagoda,Old Kelani Bridge,Sirimavo Bandaranaika Road (Bloemendhal),Pettah,Fort|Fort,Pettah,Sirimavo Bandaranaika Road (Bloemendhal),Old Kelani Bridge,Peliyagoda,Wattala,Elakanda  \n 107|Elakanda,Wattala,Peliyagoda,Old Kelani Bridge,Sirimavo Bandaranaika Road (Bloemendhal),Pettah,Fort|Fort,Pettah,Sirimavo Bandaranaika Road (Bloemendhal),Old Kelani Bridge,Peliyagoda,Wattala,Elakanda   \n 112|Maharagama,Navinna,Delkanda,Nugegoda,Kirillapone,Havlock Town,Dickmans Road,Bambalapitiya,Kollupitiya,Galle Face,Fort,Kochchikade,Kotahena|Kotahena,Kochchikade,Fort,Galle Face,Kollupitiya,Bambalapitiya,Dickmans Road,Havlock Town,Kirillapone,Nugegoda,Delkanda,Navinna,Maharagama   \n 113|Udahamulla,Embuldeniya Junction,Jubilee Post (Mirihana),Nugegoda,Kirillapone,Havlock Town,Thimbirigasyaya Junction,Thimbirigasyaya Road,Torrington Avenue,Independence Square,Cinnamon Gardens,Town Hall,Ibbanwala Junction,T.B. Jayah Road (Darley Road),Gamani Hall Junction,D.R. Wijewardena Road,Lake House,Fort,Pettah|Pettah,Fort,Lake House,D.R. Wijewardena Road,Gamani Hall Junction,T.B. Jayah Road (Darley Road),Ibbanwala Junction,Town Hall,Cinnamon Gardens,Independence Square,Torrington Avenue,Thimbirigasyaya Road,Thimbirigasyaya Junction,Havlock Town,Kirillapone,Nugegoda,Jubilee Post (Mirihana),Embuldeniya Junction,Udahamulla   \n 119|Horana,Pokunuwita,Gonapola,Kahatuduwa,Kesbewa,Piliyandala,Boralesgamuwa,Rattanapitiya,Pepiliyana,Kohuwala,Dutugemunu Street,Pamankada,Havlock Town,Thimbirigasyaya Junction,Thummulla,Kumaratunga Munidasa Road,Cambridge Place,Public Library,Dharmapala Mawatha,Reid Avenue,Cinnamon Gardens,Town Hall,Ibbanwala Junction,T.B. Jayah Road (Darley Road),Gamani Hall Junction,D.r. Wijewardena Road,Lake House,Fort,Pettah|Pettah,Fort,Lake House,D.r. Wijewardena Road,Gamani Hall Junction,T.B. Jayah Road (Darley Road),Ibbanwala Junction,Town Hall,Cinnamon Gardens,Reid Avenue,Dharmapala Mawatha,Public Library,Cambridge Place,Kumaratunga Munidasa Road,Thummulla,Thimbirigasyaya Junction,Havlock Town,Pamankada,Dutugemunu Street,Kohuwala,Pepiliyana,Rattanapitiya,Boralesgamuwa,Piliyandala,Kesbewa,Kahatuduwa,Gonapola,Pokunuwita,Horana   \n 120|Horana,Pokunuwita,Gonapola,Kahatuduwa,Kesbewa,Piliyandala,Boralesgamuwa,Rattanapitiya,Pepiliyana,Kohuwala,Dutugemunu Street,Pamankada,Havlock Town,Thimbirigasyaya Junction,Thummulla,Town Hall,Ibbanwala Junction,T.B. Jayah Road (Darley Road),Gamani Hall Junction,D.r. Wijewardena Road,Lake House,Fort,Pettah|Pettah,Fort,Lake House,D.r. Wijewardena Road,Gamani Hall Junction,T.B. Jayah Road (Darley Road),Ibbanwala Junction,Town Hall,Thummulla,Thimbirigasyaya Junction,Havlock Town,Pamankada,Dutugemunu Street,Kohuwala,Pepiliyana,Rattanapitiya,Boralesgamuwa,Piliyandala,Kesbewa,Kahatuduwa,Gonapola,Pokunuwita,Horana   \n 122|Avissawella,Kosgama,Kalu-aggala,Pahatgama,Meepe,Watareka/Meegoda,Godagama,Homagama,Makumbura,Kottawa,Pannipitiya,Maharagama,Navinna,Delkanda,Nugegoda,Kirillapone,Havlock Town,Thimbirigasyaya Junction,Thummulla,Town Hall,Ibbanwala Junction,T.B. Jayah Road (Darley Road),Gamani Hall Junction,D.r. Wijewardena Road,Lake House,Fort,Pettah|Pettah,Fort,Lake House,D.r. Wijewardena Road,Gamani Hall Junction,T.B. Jayah Road (Darley Road),Ibbanwala Junction,Town Hall,Thummulla,Thimbirigasyaya Junction,Havlock Town,Kirillapone,Nugegoda,Delkanda,Navinna,Maharagama,Pannipitiya,Kottawa,Makumbura,Homagama,Godagama,Watareka/Meegoda,Meepe,Pahatgama,Kalu-aggala,Kosgama,Avissawella   \n 125|Ingiriya,Handapangoda,Padukka,Meegoda,Godagama,Homagama,Makumbura,Kottawa,Pannipitiya,Maharagama,Navinna,Delkanda,Nugegoda,Kirillapone,Havlock Town,Thimbirigasyaya Junction,Thummulla,Town Hall,Ibbanwala Junction,T.B. Jayah Road (Darley Road),Gamani Hall Junction,D.r. Wijewardena Road,Lake House,Fort,Pettah|Pettah,Fort,Lake House,D.r. Wijewardena Road,Gamani Hall Junction,T.B. Jayah Road (Darley Road),Ibbanwala Junction,Town Hall,Thummulla,Thimbirigasyaya Junction,Havlock Town,Kirillapone,Nugegoda,Delkanda,Navinna,Maharagama,Pannipitiya,Kottawa,Makumbura,Homagama,Godagama,Meegoda,Padukka,Handapangoda,Ingiriya   \n 127|Piliyandala,Kesbewa,Kahatuduwa,Moragahahena|Moragahahena,Kahatuduwa,Kesbewa,Piliyandala  \n 138|Homagama,Kottawa,Pannipitiya,Maharagama,Navinna,Delkanda,Nugegoda,Kirillapone,Havlock Town,Thimbirigasyaya Junction,Thummulla,Kumaratunga Munidasa Road,Cambridge Place,Public Library,Dharmapala Mawatha,Reid Avenue,Independence Avenue,Albert Crescent,Public Library,Dharmapala Mawatha,Town Hall,Ibbanwala Junction,Slave Island,Lake House,Fort,Pettah|Pettah,Fort,Lake House,Slave Island,Ibbanwala Junction,Town Hall,Dharmapala Mawatha],Public Library,Albert Crescent,Independence Avenue,Reid Avenue,Dharmapala Mawatha,Public Library,Cambridge Place,Kumaratunga Munidasa Road,Thummulla,Thimbirigasyaya Junction,Havlock Town,Kirillapone,Nugegoda,Delkanda,Navinna,Maharagama,Pannipitiya,Kottawa,Homagama   \n 139|Piliyandala,Bokundara,Werahara,Boralesgamuwa,Rattanapitiya,Pepiliyana,Kohuwala|Kohuwala,Pepiliyana,Rattanapitiya,Boralesgamuwa,Werahara,Bokundara,Piliyandala  \n 140|Wellampitiya,Kolonnawa Road,Dematagoda,Sri Vajiragnana Road,Maradana Road,Paranawadiya,Norris Canal Road,Town Hall,Dharmapala Mawatha,Kollupitiya|Kollupitiya,Dharmapala Mawatha,Town Hall,Norris Canal Road,Paranawadiya,Maradana Road,Sri Vajiragnana Road,Dematagoda,Kolonnawa Road,Wellampitiya   \n 141|Wellawatte,W.A. Silva Road,Pamankada Road,Kirulapone,Baseline Road,Narahenpita|Narahenpita,Baseline Road,Kirulapone,Pamankada Road,W.A. Silva Road,Wellawatte  \n 142|Moratuwa,Koralawella,Egodauyana,Panadura|Panadura,Egodauyana,Koralawella,Moratuwa  \n 143|Hanwella,Ranala,Kaduwela,Angoda,Wellampitiya,Orugodawatta,Kosgas Handiya,Armor Street,Pettah|Pettah,Armor Street,Kosgas Handiya,Orugodawatta,Wellampitiya,Angoda,Kaduwela,Ranala,Hanwella   \n 144|Rajagiriya,Kotte Road,Borella,Punchi Borella,Maradana,Pettah,Fort|Fort,Pettah,Maradana,Punchi Borella,Borella,Kotte Road,Rajagiriya  \n 145|Gangaramaya,Nawaloka,Fort,Kochchikade,Hettiyawatta,Modara,Mattakkulia|Mattakkulia,Modara,Hettiyawatta,Kochchikade,Fort,Nawaloka,Gangaramaya   \n 148|Labugama,Kaluaggala,Hanwella,Ranala,Kaduwela,Pettah|Pettah,Kaduwela,Ranala,Hanwella,Kaluaggala,Labugama   \n 150|Kelani temple,Kelanimulla,Angoda,Aggona Junction,Kalapaluwawa,Ambagaha Junction,Welikada (Rajagiriya),Ayurveda Junction,Cotta Road,Borella,Town Hall,Park Street,Braybrook Street,Muttiah Road,Gangaramaya,Seemamalakaya|Seemamalakaya,Gangaramaya,Muttiah Road,Braybrook Street,Park Street,Town Hall,Borella,Cotta Road,Ayurveda Junction,Welikada (Rajagiriya),Ambagaha Junction,Kalapaluwawa,Aggona Junction,Angoda,Kelanimulla,Kelani temple   \n 152|Angoda,Gothatuwa,Kolonnawa,Dematagoda,Kosgas Handiya,Armor Street|Armor Street,Kosgas Handiya,Dematagoda,Kolonnawa,Gothatuwa,Angoda   \n 153|Rajagiriya,Ethulkotte,Pitakotte|Pitakotte,Ethulkotte,Rajagiriya  \n 154|Kiribathgoda,Kelaniya,Peliyagoda,Orugodawatta,Dematagoda,Borella,Cemetery Junction(Kanatta Handiya),Wijerama Mawatha,Jawatta,Royal College,Thummulla,Bambalapitiya,Wellawatte,Mt Lavinia(Galkissa),Angulana|Angulana,Mt Lavinia(Galkissa),Wellawatte,Bambalapitiya,Thummulla,Royal College,Jawatta,Wijerama Mawatha,Cemetery Junction(Kanatta Handiya),Borella,Dematagoda,Orugodawatta,Peliyagoda,Kelaniya,Kiribathgoda   \n 155|Mt. Lavania,Dehiwala,Bambalapitiya,Colombo Campus,Town Hall,Maradana,Armor Street,Kotahena,Modara|Modara,Kotahena,Armor Street,Maradana,Town Hall,Colombo Campus,Bambalapitiya,Dehiwala,Mt. Lavania  \n 157|Piliyandala,Batakettara,Kotagedara,Madapatha,Kahapola|Kahapola,Madapatha,Kotagedara,Batakettara,Piliyandala   \n 158|Piliyandala,Suwarapola,Katubedda,Moratuwa|Moratuwa,Katubedda,Suwarapola,Piliyandala   \n 159|Piliyandala,Miriswaththa,Kesbawa,Halpita,Polgasowita,Ambalangoda,Palagama|Palagama,Ambalangoda,Polgasowita,Halpita,Kesbawa,Miriswaththa,Piliyandala  \n 160|Ethul Kotte,Rajagiriya,Borella,Ward Place,Town Hall,Fort,Kochchikade,Modara,Mattakkuliya|Mattakkuliya,Modara,Kochchikade,Fort,Town Hall,Ward Place,Borella,Rajagiriya,Ethul Kotte  \n 164|Salmal Uyana,Angoda,Kotikawatte,Wellampitiya,Kolonnawa,Dematagoda,Baseline Road,Borella,Maradana Rd,Carey College,Town Hall|Town Hall,Carey College,Maradana Rd,Borella,Baseline Road,Dematagoda,Kolonnawa,Wellampitiya,Kotikawatte,Angoda,Salmal Uyana    \n 166|Angoda,Kotikawatte,Wellampitiya,Kolonnawa,Dematagoda,Nalanda College,Punchi Borella,Carey College,Town Hall,Park St,Gangaramaya,Slave Island|Slave Island,Gangaramaya,Park St,Town Hall,Carey College,Punchi Borella,Nalanda College,Dematagoda,Kolonnawa,Wellampitiya,Kotikawatte,Angoda  \n 168|Pita Kotte,Ethul Kotte,Rajagiriya,Ayurveda Junction,Borella,Ward Place,Union Place,Slave Island,Fort,Kochchikade|Kochchikade,Fort,Slave Island,Union Place,Ward Place,Borella,Ayurveda Junction,Rajagiriya,Ethul Kotte,Pita Kotte  \n 170|Pettah,Rajagiriya,Battaramulla,Thalangama,Kaduwela,Malabe,Athurugiriya|Athurugiriya,Malabe,Kaduwela,Thalangama,Battaramulla,Rajagiriya,Pettah  \n 171|Fort,Pettah,Hultsdorf,Technical Junction,Maradana,Punchi Borella,Borella,Cotta Road,Ayurveda Junction,Rajagiriya,Welikada,Battaramulla,Koswatte,Korambe,Kandawatta Junction|Kandawatta Junction,Korambe,Koswatte,Battaramulla,Welikada,Rajagiriya,Ayurveda Junction,Cotta Road,Borella,Punchi Borella,Maradana,Technical Junction,Hultsdorf,Pettah,Fort   \n 174|Kottawa,Pannipitiya,Polwatta,Thalawathugoda,Wickramasinghepura,Pelawatta,Battaramulla,Welikada,Rajagiriya,Cotta Road,Ayurveda Junction,Borella|Borella,Ayurveda Junction,Cotta Road,Rajagiriya,Welikada,Battaramulla,Pelawatta,Wickramasinghepura,Thalawathugoda,Polwatta,Pannipitiya,Kottawatte,Rajagiriya,Nawala,Narahenpita,Kirulapana,Havlock Town,Dickmans Road,Wellawatte,Dehiwala,Mattakkuliya|Mattakkuliya,Dehiwala,Wellawatte,Dickmans Road,Havlock Town,Kirulapana,Narahenpita,Nawala,Rajagiriya,Ethul Kotte   \n 200|Kiribathgoda,Kadawatha,Kirillawala,Miriswatta|Miriswatta,Kirillawala,Kadawatha,Kiribathgoda   \n 218|Kiribathgoda,Kadawatha,KokisKade Junction|KokisKade Junction,Kadawatha,Kiribathgoda   \n 231|Radawana,Yakkala,Kirillawala,Kadawatha,Kiribathgoda|Kiribathgoda,Kadawatha,Kirillawala,Yakkala,Radawana   \n 219|Panagoda,Meegoda (High level Road),Artigala Mawatha,Lo level Road,Hanwella|Hanwella,Lo level Road,Artigala Mawatha,Meegoda (High level Road),Panagoda   \n 240|Negombo,Kattuwa,Katunayake ( airport ) Junction,Sedduwa,Ja-Ela,Kandana,wattala,Thotilanga,Pettah|Pettah,Thotilanga,wattala,Kandana,Ja-Ela,Sedduwa,Katunayake ( airport ) Junction,Kattuwa,Negombo   \n 242|Divulapitiya-Marandagahamula-Dunagaha-Miriswattha-Kadirana-Y Junction-Coppara Junction-Negombo|Divulapitiya-Marandagahamula-Dunagaha-Miriswattha-Kadirana-Y Junction-Coppara Junction-Negombo  \n 255|Mt. Lavinia,Katubedda,Piliyandala,Miriswatta Junction,Kottawa|Kottawa,Miriswatta Junction,Piliyandala,Katubedda,Mt. Lavinia   \n 256|Ratmalana,Borupana|Borupana,Ratmalana   \n 259|Nugegoda,Thalapathpitiya|Thalapathpitiya,Nugegoda   \n 265|Minuwangoda,Kotugoda,Ekala,Ja-Ela,Mahabage,Wattala,Paliyagoda,Pettah|Pettah,Paliyagoda,Wattala,Mahabage,Ja-Ela,Ekala,Kotugoda,Minuwangoda    \n 280|Pannipitiya,Kottawa,Pinhena Junction,Dolekade Junction,Diyagama,Kiriwaththuduwa,Thalagala,Moragahahena,Perengiyawela|Perengiyawela,Moragahahena,Thalagala,Kiriwaththuduwa,Diyagama,Dolekade Junction,Pinhena Junction,Kottawa,Pannipitiya   \n 313|Kalubowila,Homagama|Homagama,Kalubowila  \n 333|Kiribathgoda,Kadawatha,Kirillawala|Kirillawala,Kadawatha,Kiribathgoda   \n 336|Kottawa,Pola Junction,Water tank Junction,Hatharaman Junction,Podi Kade,Horahena Junction,Sirimalwatta,Katukurunda,Wasana mawatha,Vidyala Junction,Hokandara,Arangala,Malabe|Malabe,Arangala,Hokandara,Vidyala Junction,Wasana mawatha,Katukurunda,Sirimalwatta,Horahena Junction,Podi Kade,Hatharaman Junction,Water tank Junction,Pola Junction,Kottawa   \n 342|Piliyandala,Kesbawa,Halpita,Polgasowita,Siyambalagoda,Mathtegoda,Kottawa|Kottawa,Mathtegoda,Siyambalagoda,Polgasowita,Halpita,Kesbawa,Piliyandala  \n 430|Pettah,Galle Face,Kollupitiya,Bambalapitiya,Wellawatte,Dehiwala,Mt Lavinia,Moratuwa,New Galle Rd,Panadura,Wadduwa,Waskaduwa,Kalutara,Katukurunda,Nagoda,Kalutara General Hospital,Dodangoda,Matugama|Matugama,Dodangoda,Kalutara General Hospital,Nagoda,Katukurunda,Kalutara,Waskaduwa,Wadduwa,Panadura,New Galle Rd,Moratuwa,Mt Lavinia,Dehiwala,Wellawatte,Bambalapitiya,Kollupitiya,Galle Face,Pettah  \n 446|Katukurunda,Nagoda,Thudugala Junct.,Wellatha,Neboda|Neboda,Wellatha,Thudugala Junct.,Nagoda,Katukurunda  \n 450|Panadura,Wekada,Eluwila,Alubomulla,Bandaragama,Gelanigama,Pokunuwita,Horana,Ingiriya,Idangoda,Rathnapura|Rathnapura,Idangoda,Ingiriya,Horana,Pokunuwita,Gelanigama,Bandaragama,Alubomulla,Eluwila,Wekada,Panadura    \n 457|Panadura,Hospital Junction,Hirana Rd,Thanathirimulla,Hirana Thotupola,Kindelpitya Temple Junction|Kindelpitya Temple Junction,Hirana Thotupola,Thanathirimulla,Hirana Rd,Hospital Junction,Panadura    \n 459|Bandaragama,Raigama Junct.,Halthota,Millaniya,Keselhenawa,Madurawala,Anguruwathota|Anguruwathota,Madurawala,Keselhenawa,Millaniya,Halthota,Raigama Junct.,Bandaragama   \n 490|Horawala,Welipenna,Lewwanduwa,Ittapana,Galatara|Galatara,Ittapana,Lewwanduwa,Welipenna,Horawala   \n 698|Ranala,Mullegama,Habarakada,Podi Athurugiriya,Homagama|Homagama,Podi Athurugiriya,Habarakada,Mullegama,Ranala    \n 714|Thalahena,Himbutana,Galwana,Angoda,Kotikawatte,Kolonnawa,Dematagoda,Borella|Borella,Dematagoda,Kolonnawa,Kotikawatte,Angoda,Galwana,Himbutana,Thalahena        \n 990|Temple Road,Dewram Wehera,Ambagas hathara,Weera Mawatha,Vidyala Junction|Vidyala Junction,Weera Mawatha,Ambagas hathara,Dewram Wehera,Temple Road   \n 993|Maharagama,Old Road,Pannipitiya(Moraketiya),Moragahakanda,Liyanagoda,Vidyala Junction,Hokandara,Arangala,Malambe|Malambe,Arangala,Hokandara,Vidyala Junction,Liyanagoda,Moragahakanda,Pannipitiya(Moraketiya),Old Road,Maharagama";
            //lines = line.Split('\n');
            //temp = line.Split('|');
            //route = new Route(temp[0]);
            //route = processInput(temp[1], route);
            //route = processInput(temp[2], route);
            //routes.Add(temp[0], route);

            //Clear the buffers
            routes.Clear();
            //Read line by line to get the route number and the locations through the route
            for (int i = 0; i < lines.Length; i++)
            {
                temp = lines[i].Split('|');
                route = new Route(temp[0]);
                //Debug.WriteLine(temp[0] + "-------" + temp[1] + "------------" + temp[2]);
                route = processInput(temp[1], route);
                route = processInput(temp[2], route);
                
                //if else statements for preventing any exceptions due to duplicate key values
                if (!routes.ContainsKey(temp[0]))
                {
                    routes.Add(temp[0], route);
                   // Debug.WriteLine("added "+temp[0]);
                }
                else
                {
                   // Debug.WriteLine("not"+ temp[0]);
                }
            }

        }

             
        //Add the data into the data structures
        private static Route processInput(String temp, Route route) {
            Location location = null;
            String[] tempLocations = temp.Split(',');

            //fill the dictionarires for routes and locations
            foreach (String s in tempLocations) {
                if (!locations.ContainsKey(s)) {
                    location = new Location(s);
                    route.addLocationToRouteIn(location);
                    locations.Add(s, location);
                } else {
                    Location value;
                    if (locations.TryGetValue(s, out value)) {
                        route.addLocationToRouteIn(locations[s]);
                    }
                }
            }
            return route;
        }

        //Get the locations dictionary
        public Dictionary<String, Location> getAllLocations() {
            return locations;
        }

        //Get the routes dictionary
        public Dictionary<String, Route> getAllRoutes() {
            return routes;
        }
    }
}
