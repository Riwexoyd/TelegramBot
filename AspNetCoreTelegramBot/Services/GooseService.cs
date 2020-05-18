﻿using System;

namespace AspNetCoreTelegramBot.Services
{
    public class GooseService : IGooseService
    {
        private static readonly string[] geese =
        {
            @"
░░░░░░░░░░░░░░░░░░░░
░░░░░ЗАПУСКАЕМ░░░░░░░
░ГУСЯ░▄▀▀▀▄░РАБОТЯГИ░░
▄███▀░◐░░░▌░░░░░░░░░
░░░░▌░░░░░▐░░░░░░░░░
░░░░▐░░░░░▐░░░░░░░░░
░░░░▌░░░░░▐▄▄░░░░░░░
░░░░▌░░░░▄▀▒▒▀▀▀▀▄
░░░▐░░░░▐▒▒▒▒▒▒▒▒▀▀▄
░░░▐░░░░▐▄▒▒▒▒▒▒▒▒▒▒▀▄
░░░░▀▄░░░░▀▄▒▒▒▒▒▒▒▒▒▒▀▄
░░░░░░▀▄▄▄▄▄█▄▄▄▄▄▄▄▄▄▄▄▀▄
░░░░░░░░░░░▌▌░▌▌░░░░░
░░░░░░░░░░░▌▌░▌▌░░░░░
░░░░░░░░░▄▄▌▌▄▌▌░░░░░
",
            @"
░░░░░░░░░░░░░░░░░░░░
░ВЗГЛЯНИ ░░ВОКРУГ,░░░░░
░ОГЛЯНИСЬ░НАЗАД!░░░░░░
░ГУСИ░▄▀▀▀▄░С░ТОБОЮ░░
░░░░▀░░░◐░▀███▄░░░░░
░░░░▌░░░░░▐░░░░░░░░░
░░░░▐░░░░░▐░░░░░░░░░
░░░░▌░░░░░▐▄▄░░░░░░░
░░░░▌░░░░▄▀▒▒▀▀▀▀▄
░░░▐░░░░▐▒▒▒▒▒▒▒▒▀▀▄
░░░▐░░░░▐▄▒▒▒▒▒▒▒▒▒▒▀▄
░░░░▀▄░░░░▀▄▒▒▒▒▒▒▒▒▒▒▀▄
░░░░░░▀▄▄▄▄▄█▄▄▄▄▄▄▄▄▄▄▄▀▄
░СВЯЗАТЬСЯ░░▌▌░▌▌░░░░░
░░░ХОТЯТ░░░░▌▌░▌▌░░░░░
░░░░░░░░░░░▄▄▌▌▄▌▌░░
",
            @"

░░░░░░░▄▀▀▄░░░░░░░░░░░░
░░░░░▄▀▒▒▒▒▀▄░ЗАПУСКАЕМ░░
░░░░░░▀▌▒▒▐▀░░ГУСЕПЕТУХА░░
▄███▀░◐░░░▌░░░РАБОТЯГИ░░░
░░▐▀▌░░░░░▐░░░░░░░░░▄▀▀▀▄▄
░▐░░▐░░░░░▐░░░░░░░░░█░▄█▀
░▐▄▄▌░░░░░▐▄▄░░░░░░█░░▄▄▀▀▀▀▄
░░░░▌░░░░▄▀▒▒▀▀▀▀▄▀░▄▀░▄▄▄▀▀
░░░▐░░░░▐▒▒▒▒▒▒▒▒▀▀▄░░▀▄▄▄░▄
░░░▐░░░░▐▄▒▒▒▒▒▒▒▒▒▒▀▄░▄▄▀▀
░░░░▀▄░░░░▀▄▒▒▒▒▒▒▒▒▒▒▀▄░
░░░░░▀▄▄░░░█▄▄▄▄▄▄▄▄▄▄▄▀▄
░░░░░░░░▀▀▀▄▄▄▄▄▄▄▄▀▀░
░░░░░░░░░░░▌▌░▌▌
░░░░░░░░░▄▄▌▌▄▌▌
",
            @"

░░░░░░░░░░░░░░░░░░░░
░ЗАПУСКАЕМ░░ГУСЯ-ГИДРУ░
░░░░░░РАБОТЯГИ░░░░░░░
░░░░░░▄▀▀▀▄░░░░░░░░░
▄███▀░◐░▄▀▀▀▄░░░░░░░
░░▄███▀░◐░░░░▌░░░░░░
░░░▌░▄▀▀▀▄░░░▌░░░░░░
▄███▀░◐░░░▌░░▌░░░░░░
░░░░▌░░░░░▐▄▄▌░░░░░░
░░░░▌░░░░░▐▄▄░░░░░░░
░░░░▌░░░░▄▀▒▒▀▀▀▀▄░░
░░░▐░░░░▐▒▒▒▒▒▒▒▒▀▀▄
░░░▐░░░░▐▄▒▒▒▒▒▒▒▒▒▒▀▄
░░░░▀▄░░░░▀▄▒▒▒▒▒▒▒▒▒▒▀▄
░░░░░░▀▄▄▄▄▄█▄▄▄▄▄▄▄▄▄▄▄▀▄
░░░░░░░░░░░▌▌░▌▌░░░░░
░░░░░░░░░░░▌▌░▌▌░░░░░
░░░░░░░░░▄▄▌▌▄▌▌░░░░░
",
            @"

░░░░░░░░░░░░░░░░░░░░░░
░░ЗАПУСКАЕМ░░МОЩНОГО░░░
░░░░░░░ГУСЯ-ГИДРУ░░░░░░░
░░░░░▄▀▀▀▄░░░░░░░░ ▄▄░░
▄███▀░◐░▄▀▀▀▄░░░▄▀░░█░
░░▄███▀░◐░░░░▌▄▀░░▄▀░░
░░░░▐░▄▀▀▀▄░░░░░▄▀░░░░
▄███▀░◐░░░░▌░░░░░▀▄░░░
▄▄░▌░░░░░▐▄▄▀▄▄▀▄▀░░░░░░░▄▄
█░░▀▄░░░░▄▀▒▒▀▀▀▀▄░░░░░▄▀░░█
░▀▄░░▀▄░▐▒▒▒▒▒▒▒▒▀▀▄▄▄▄▀░░▄▀
░░░▀▄░░▀▐▄▒▒▒▒▒▒▒▒▒▒▒▒░░▄▀
░░░▄▀░░░░░▀▄▒▒▒▒▒▒▒▒▒▒▒▀▄
░░░▀▄▀▄▄▀▄▄▄█▄▄▄▄▄▄▄▀▄▄▀▄▀
░░░░░░░▌▌░▌▌░▌▌░▌▌░░▌▌░▌▌░
░░░░░░░▌▌░▌▌░▌▌░▌▌░░▌▌░▌▌░
░░░░░▄▄▌▌▄▌▌▄▌▌▄▌▌░▄▌▌▄▌▌░
",
            @"
──────▄▌▐▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▌
───▄▄██▌█░ВЕЗЁМ▄▀▀▀▄░ГУСЕЙ░░░░░░░
───████▌█▄███▀░◐░▄▀▀▀▄░░РАБОТЯГИ░
──██░░█▌█░░▄███▀░◐░░▄▀▀▀▄░░░░░░░
─██░░░█▌█░░░░▐░▄▀▀▀▌░░░░◐░▀███▄░
▄██████▌█▄███▀░◐░░░▌░░░░░▐░░░░░░
███████▌█░░░░▌░░░░░▌░░░░░▐░░░░░░
███████▌█▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▌
""▀(@)▀▀▀▀▀▀▀(@)(@)▀▀▀▀▀▀▀▀▀▀▀▀▀(@)▀(@)""
",
            @"

░░░░░▄▀▀▀▄▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▌
▄███▀░◐░░░▌░ЗАПУСКАЕМ░ГУСЯ-ФУРУ░░░
████▌░░░░░▐▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▌
""▀(@)▀▀▀▀▀▀▀(@)(@)▀▀▀▀▀▀▀▀▀▀▀▀▀(@)▀""
",
            @"

░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░
░░░░ЗАПУСКАЕМ░ГУСЕЙ-РАЗВЕДЧИКОВ░░░░
░░░░░▄▀▀▀▄░░░▄▀▀▀▀▄░░░▄▀▀▀▄░░░░░
▄███▀░◐░░░▌░▐0░░░░0▌░▐░░░◐░▀███▄
░░░░▌░░░░░▐░▌░▐▀▀▌░▐░▌░░░░░▐░░░░
░░░░▐░░░░░▐░▌░▌▒▒▐░▐░▌░░░░░▌░░░░
"
        };
        static readonly Random random = new Random();

        public string GetRandomGoose()
        {
            return geese[random.Next(geese.Length)];
        }
    }
}