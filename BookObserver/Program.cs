﻿using System;
using NightCodeClub;
using NightCodeClub.DataBase;

class programm {
    static void Main(string[] args) {
        var data = new JsonDataBase(); // json implementation
        var dataBase = new DataBridge(data); // bridge
        dataBase.Load();
        
        TelegramAPI.GetInstance().StartListening();
        Console.ReadLine();
        
        TelegramAPI.GetInstance().StopBot();
        dataBase.Save();
    }

}







