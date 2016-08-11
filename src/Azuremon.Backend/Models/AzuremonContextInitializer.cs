using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Azuremon.DataObjects;

namespace Azuremon.Backend.Models
{
    public class AzuremonContextInitializer : DropCreateDatabaseIfModelChanges<AzuremonContext>
    {

        #region [ Categories ]

        private string Cats = @"Grass,#78c850
Poison,#a040a0
Fire,#f08030
Flying,#a890f0
Water,#6890f0
Bug,#a8b820
Normal,#8a8a59
Electric,#f8d030
Groud,#e0c068
Fairy,#e898e8
Fighting,#c03028
Psychic,#f85888
Rock,#b8a038
Steel,#b8b8d0
Ice,#98d8d8
Ghost,#705898
Dragon,#7038f8";

        #endregion

        #region [ Pokemon ]

        private string Pokemon = @"001,Bulbasaur,Grass; Poison,90,126,126,16,10,25,Tackle; Vine Whip,Power Whip; Seed Bomb; Sludge Bomb
002,Ivysaur,Grass; Poison,120,156,158,8,7,100,Razor Leaf; Vine Whip,Power Whip; Sludge Bomb; Solar Beam
003,Venusaur,Grass; Poison,160,198,200,4,5,—,Razor Leaf; Vine Whip,Petal Blizzard; Sludge Bomb; Solar Beam
004,Charmander,Fire,78,128,108,16,10,25,Ember; Scratch,Flame Burst; Flame Charge; Flamethrower
005,Charmeleon,Fire,116,160,140,8,7,100,Ember; Scratch,Fire Punch; Flame Burst; Flamethrower
006,Charizard,Fire; Flying,156,212,182,4,5,—,Ember; Wing Attack,Dragon Claw; Fire Blast; Flamethrower
007,Squirtle,Water,88,112,142,16,10,25,Bubble; Tackle,Aqua Jet; Aqua Tail; Water Pulse
008,Wartortle,Water,118,144,176,8,7,100,Bite; Water Gun,Aqua Jet; Hydro Pump; Ice Beam
009,Blastoise,Water,158,186,222,4,5,—,Bite; Water Gun,Flash Cannon; Hydro Pump; Ice Beam
010,Caterpie,Bug,90,62,66,40,20,12,Bug Bite; Tackle,Struggle
011,Metapod,Bug,100,56,86,20,9,50,Bug Bite; Tackle,Struggle
012,Butterfree,Bug; Flying,120,144,144,10,6,—,Bug Bite; Confusion,Bug Buzz; Psychic; Signal Beam
013,Weedle,Bug; Poison,80,68,64,40,20,12,Bug Bite; Poison Sting,Struggle
014,Kakuna,Bug; Poison,90,62,82,20,9,50,Bug Bite; Poison Sting,Struggle
015,Beedrill,Bug; Poison,130,144,130,10,6,—,Bug Bite; Poison Jab,Aerial Ace; Sludge Bomb; X-Scissor
016,Pidgey,Normal; Flying,80,94,90,40,20,12,Quick Attack; Tackle,Aerial Ace; Air Cutter; Twister
017,Pidgeotto,Normal; Flying,126,126,122,20,9,50,Steel Wing; Wing Attack,Aerial Ace; Air Cutter; Twister
018,Pidgeot,Normal; Flying,166,170,166,10,6,—,Steel Wing; Wing Attack,Aerial Ace; Air Cutter; Hurricane
019,Rattata,Normal,60,92,86,40,20,25,Quick Attack; Tackle,Body Slam; Dig; Hyper Fang
020,Raticate,Normal,110,146,150,16,7,—,Bite; Quick Attack,Dig; Hyper Beam; Hyper Fang
021,Spearow,Normal; Flying,80,102,78,40,15,50,Peck; Quick Attack,Aerial Ace; Drill Peck; Twister
022,Fearow,Normal; Flying,130,168,146,16,7,—,Peck; Steel Wing,Aerial Ace; Drill Run; Twister
023,Ekans,Poison,70,112,112,40,15,50,Acid; Poison Sting,Gunk Shot; Sludge Bomb; Wrap
024,Arbok,Poison,120,166,166,16,7,—,Acid; Bite,Dark Pulse; Gunk Shot; Sludge Wave
025,Pikachu,Electric,70,124,108,16,10,50,Quick Attack; Thunder Shock,Discharge; Thunder; Thunderbolt
026,Raichu,Electric,120,200,154,8,6,—,Spark; Thunder Shock,Brick Break; Thunder; Thunder Punch
027,Sandshrew,Ground,100,90,114,40,10,50,Mud Shot; Scratch,Dig; Rock Slide; Rock Tomb
028,Sandslash,Ground,150,150,172,16,6,—,Metal Claw; Mud Shot,Bulldoze; Earthquake; Rock Tomb
029,Nidoran♀,Poison,110,100,104,40,15,25,Bite; Poison Sting,Body Slam; Poison Fang; Sludge Bomb
030,Nidorina,Poison,140,132,136,20,7,100,Bite; Poison Sting,Dig; Poison Fang; Sludge Bomb
031,Nidoqueen,Poison; Ground,180,184,190,10,5,—,Bite; Poison Jab,Earthquake; Sludge Wave; Stone Edge
032,Nidoran♂,Poison,92,110,94,40,15,25,Peck; Poison Sting,Body Slam; Horn Attack; Sludge Bomb
033,Nidorino,Poison,122,142,128,20,7,100,Poison Jab; Poison Sting,Dig; Horn Attack; Sludge Bomb
034,Nidoking,Poison; Ground,162,204,170,10,5,—,Fury Cutter; Poison Jab,Earthquake; Megahorn; Sludge Wave
035,Clefairy,Fairy,140,116,124,24,10,50,Pound; Zen Headbutt,Body Slam; Disarming Voice; Moonblast
036,Clefable,Fairy,190,178,178,8,6,—,Pound; Zen Headbutt,Dazzling Gleam; Moonblast; Psychic
037,Vulpix,Fire,76,106,118,24,10,50,Ember; Quick Attack,Body Slam; Flame Charge; Flamethrower
038,Ninetales,Fire,146,176,194,8,6,—,Ember; Feint Attack,Fire Blast; Flamethrower; Heat Wave
039,Jigglypuff,Normal; Fairy,230,98,54,40,10,50,Feint Attack; Pound,Body Slam; Disarming Voice; Play Rough
040,Wigglytuff,Normal; Fairy,280,168,108,16,6,—,Feint Attack; Pound,Dazzling Gleam; Hyper Beam; Play Rough
041,Zubat,Poison; Flying,80,88,90,40,20,50,Bite; Quick Attack,Air Cutter; Poison Fang; Sludge Bomb
042,Golbat,Poison; Flying,150,164,164,16,7,—,Bite; Wing Attack,Air Cutter; Ominous Wind; Poison Fang
043,Oddish,Grass; Poison,90,134,130,48,15,25,Acid; Razor Leaf,Moonblast; Seed Bomb; Sludge Bomb
044,Gloom,Grass; Poison,120,162,158,24,7,100,Acid; Razor Leaf,Moonblast; Petal Blizzard; Sludge Bomb
045,Vileplume,Grass; Poison,150,202,190,12,5,—,Acid; Razor Leaf,Moonblast; Petal Blizzard; Solar Beam
046,Paras,Bug; Grass,70,122,120,32,15,50,Bug Bite; Scratch,Cross Poison; Seed Bomb; X-Scissor
047,Parasect,Bug; Grass,120,162,170,16,7,—,Bug Bite; Fury Cutter,Cross Poison; Solar Beam; X-Scissor
048,Venonat,Bug; Poison,120,108,118,40,15,50,Bug Bite; Confusion,Dazzling Gleam; Psybeam; Shadow Ball
049,Venomoth,Bug; Poison,140,172,154,16,7,—,Bug Bite; Confusion,Bug Buzz; Poison Fang; Psychic
050,Diglett,Ground,20,108,86,40,10,50,Mud Shot; Scratch,Dig; Mud Bomb; Rock Tomb
051,Dugtrio,Ground,70,148,140,16,6,—,Mud Shot; Sucker Punch,Earthquake; Mud Bomb; Stone Edge
052,Meowth,Normal,80,104,94,40,15,50,Bite; Scratch,Body Slam; Dark Pulse; Night Slash
053,Persian,Normal,130,156,146,16,7,—,Feint Attack; Scratch,Night Slash; Play Rough; Power Gem
054,Psyduck,Water,100,132,112,40,10,50,Water Gun; Zen Headbutt,Aqua Tail; Cross Chop; Psybeam
055,Golduck,Water,160,194,176,16,6,—,Confusion; Water Gun,Hydro Pump; Ice Beam; Psychic
056,Mankey,Fighting,80,122,96,40,10,50,Karate Chop; Scratch,Brick Break; Cross Chop; Low Sweep
057,Primeape,Fighting,130,178,150,16,6,—,Karate Chop; Low Kick,Cross Chop; Low Sweep; Night Slash
058,Growlithe,Fire,110,156,110,24,10,50,Bite; Ember,Body Slam; Flame Wheel; Flamethrower
059,Arcanine,Fire,180,230,180,8,6,—,Bite; Fire Fang,Bulldoze; Fire Blast; Flamethrower
060,Poliwag,Water,80,108,98,40,15,25,Bubble; Mud Shot,Body Slam; Bubble Beam; Mud Bomb
061,Poliwhirl,Water,130,132,132,20,7,100,Bubble; Mud Shot,Bubble Beam; Mud Bomb; Scald
062,Poliwrath,Water; Fighting,180,180,202,10,5,—,Bubble; Mud Shot,Hydro Pump; Ice Punch; Submission
063,Abra,Psychic,50,110,76,40,99,25,Zen Headbutt,Psyshock; Shadow Ball; Signal Beam
064,Kadabra,Psychic,80,150,112,20,7,100,Confusion; Psycho Cut,Dazzling Gleam; Psybeam; Shadow Ball
065,Alakazam,Psychic,110,186,152,10,5,—,Confusion; Psycho Cut,Dazzling Gleam; Psychic; Shadow Ball
066,Machop,Fighting,140,118,96,40,10,25,Karate Chop; Low Kick,Brick Break; Cross Chop; Low Sweep
067,Machoke,Fighting,160,154,144,20,7,100,Karate Chop; Low Kick,Brick Break; Cross Chop; Submission
068,Machamp,Fighting,180,198,180,10,5,—,Bullet Punch; Karate Chop,Cross Chop; Stone Edge; Submission
069,Bellsprout,Grass; Poison,100,158,78,40,15,25,Acid; Vine Whip,Power Whip; Sludge Bomb; Wrap
070,Weepinbell,Grass; Poison,130,190,110,20,7,100,Acid; Razor Leaf,Power Whip; Seed Bomb; Sludge Bomb
071,Victreebel,Grass; Poison,160,222,152,10,5,—,Acid; Razor Leaf,Leaf Blade; Sludge Bomb; Solar Beam
072,Tentacool,Water; Poison,80,106,136,40,15,50,Bubble; Poison Sting,Bubble Beam; Water Pulse; Wrap
073,Tentacruel,Water; Poison,160,170,196,16,7,—,Acid; Poison Jab,Blizzard; Hydro Pump; Sludge Wave
074,Geodude,Rock; Ground,80,106,118,40,10,25,Rock Throw; Tackle,Dig; Rock Slide; Rock Tomb
075,Graveler,Rock; Ground,110,142,156,20,7,100,Mud Shot; Rock Throw,Dig; Rock Slide; Stone Edge
076,Golem,Rock; Ground,160,176,198,10,5,—,Mud Shot; Rock Throw,Ancient Power; Earthquake; Stone Edge
077,Ponyta,Fire,100,168,138,32,10,50,Ember; Tackle,Fire Blast; Flame Charge; Flame Wheel
078,Rapidash,Fire,130,200,170,12,6,—,Ember; Low Kick,Drill Run; Fire Blast; Heat Wave
079,Slowpoke,Water; Psychic,180,110,110,40,10,50,Confusion; Water Gun,Psychic; Psyshock; Water Pulse
080,Slowbro,Water; Psychic,190,184,198,16,6,—,Confusion; Water Gun,Ice Beam; Psychic; Water Pulse
081,Magnemite,Electric; Steel,50,128,138,40,10,50,Spark; Thunder Shock,Discharge; Magnet Bomb; Thunderbolt
082,Magneton,Electric; Steel,100,186,180,16,6,—,Spark; Thunder Shock,Discharge; Flash Cannon; Magnet Bomb
083,Farfetch'd,Normal; Flying,104,138,132,24,9,—,Cut; Fury Cutter,Aerial Ace; Air Cutter; Leaf Blade
084,Doduo,Normal; Flying,70,126,96,40,10,50,Peck; Quick Attack,Aerial Ace; Drill Peck; Swift
085,Dodrio,Normal; Flying,120,182,150,16,6,—,Feint Attack; Steel Wing,Aerial Ace; Air Cutter; Drill Peck
086,Seel,Water,130,104,138,40,9,50,Ice Shard; Water Gun,Aqua Jet; Aqua Tail; Icy Wind
087,Dewgong,Water; Ice,180,156,192,16,6,—,Frost Breath; Ice Shard,Aqua Jet; Blizzard; Icy Wind
088,Grimer,Poison,160,124,110,40,10,50,Acid; Mud-Slap,Mud Bomb; Sludge; Sludge Bomb
089,Muk,Poison,210,180,188,16,6,—,Acid; Poison Jab,Dark Pulse; Gunk Shot; Sludge Wave
090,Shellder,Water,60,120,112,40,10,50,Ice Shard; Tackle,Bubble Beam; Icy Wind; Water Pulse
091,Cloyster,Water; Ice,100,196,196,16,6,—,Frost Breath; Ice Shard,Blizzard; Hydro Pump; Icy Wind
092,Gastly,Ghost; Poison,60,136,82,32,10,25,Lick; Sucker Punch,Dark Pulse; Ominous Wind; Sludge Bomb
093,Haunter,Ghost; Poison,90,172,118,16,7,100,Lick; Shadow Claw,Dark Pulse; Shadow Ball; Sludge Bomb
094,Gengar,Ghost; Poison,120,204,156,8,5,—,Shadow Claw; Sucker Punch,Dark Pulse; Shadow Ball; Sludge Wave
095,Onix,Rock; Ground,70,90,186,16,9,—,Rock Throw; Tackle,Iron Head; Rock Slide; Stone Edge
096,Drowzee,Psychic,120,104,140,40,10,50,Confusion; Pound,Psybeam; Psychic; Psyshock
097,Hypno,Psychic,170,162,196,16,6,—,Confusion; Zen Headbutt,Psychic; Psyshock; Shadow Ball
098,Krabby,Water,60,116,110,40,15,50,Bubble; Mud Shot,Bubble Beam; Vice Grip; Water Pulse
099,Kingler,Water,110,178,168,16,7,—,Metal Claw; Mud Shot,Vice Grip; Water Pulse; X-Scissor
100,Voltorb,Electric,80,102,124,40,10,50,Spark; Tackle,Discharge; Signal Beam; Thunderbolt
101,Electrode,Electric,120,150,174,16,6,—,Spark; Tackle,Discharge; Hyper Beam; Thunderbolt
102,Exeggcute,Grass; Psychic,120,110,132,40,10,50,Confusion,Ancient Power; Psychic; Seed Bomb
103,Exeggutor,Grass; Psychic,190,232,164,16,6,—,Confusion; Zen Headbutt,Psychic; Seed Bomb; Solar Beam
104,Cubone,Ground,100,102,150,32,10,50,Mud-Slap; Rock Smash,Bone Club; Bulldoze; Dig
105,Marowak,Ground,120,140,202,12,6,—,Mud-Slap; Rock Smash,Bone Club; Dig; Earthquake
106,Hitmonlee,Fighting,100,148,172,16,9,—,Low Kick; Rock Smash,Low Sweep; Stomp; Stone Edge
107,Hitmonchan,Fighting,100,138,204,16,9,—,Bullet Punch; Rock Smash,Brick Break; Fire Punch; Ice Punch; Thunder Punch
108,Lickitung,Normal,180,126,160,16,9,—,Lick; Zen Headbutt,Hyper Beam; Power Whip; Stomp
109,Koffing,Poison,80,136,142,40,10,50,Acid; Tackle,Dark Pulse; Sludge; Sludge Bomb
110,Weezing,Poison,130,190,198,16,6,—,Acid; Tackle,Dark Pulse; Shadow Ball; Sludge Bomb
111,Rhyhorn,Ground; Rock,160,110,116,40,10,50,Mud-Slap; Rock Smash,Bulldoze; Horn Attack; Stomp
112,Rhydon,Ground; Rock,210,166,160,16,6,—,Mud-Slap; Rock Smash,Earthquake; Megahorn; Stone Edge
113,Chansey,Normal,500,40,60,16,9,—,Pound; Zen Headbutt,Dazzling Gleam; Psybeam; Psychic
114,Tangela,Grass,130,164,152,32,9,—,Vine Whip,Power Whip; Sludge Bomb; Solar Beam
115,Kangaskhan,Normal,210,142,178,16,9,—,Low Kick; Mud-Slap,Brick Break; Earthquake; Stomp
116,Horsea,Water,60,122,100,40,10,50,Bubble; Water Gun,Bubble Beam; Dragon Pulse; Flash Cannon
117,Seadra,Water,110,176,150,16,6,—,Dragon Breath; Water Gun,Blizzard; Dragon Pulse; Hydro Pump
118,Goldeen,Water,90,112,126,40,15,50,Mud Shot; Peck,Aqua Tail; Horn Attack; Water Pulse
119,Seaking,Water,160,172,160,16,7,—,Peck; Poison Jab,Drill Run; Icy Wind; Megahorn
120,Staryu,Water,60,130,128,40,15,50,Quick Attack; Water Gun,Bubble Beam; Power Gem; Swift
121,Starmie,Water; Psychic,120,194,192,16,6,—,Quick Attack; Water Gun,Hydro Pump; Power Gem; Psybeam
122,Mr. Mime,Psychic; Fairy,80,154,196,24,9,—,Confusion; Zen Headbutt,Psybeam; Psychic; Shadow Ball
123,Scyther,Bug; Flying,140,176,180,24,9,—,Fury Cutter; Steel Wing,Bug Buzz; Night Slash; X-Scissor
124,Jynx,Ice; Psychic,130,172,134,24,9,—,Frost Breath; Pound,Draining Kiss; Ice Punch; Psyshock
125,Electabuzz,Electric,130,198,160,24,9,—,Low Kick; Thunder Shock,Thunder; Thunder Punch; Thunderbolt
126,Magmar,Fire,130,214,158,24,9,—,Ember; Karate Chop,Fire Blast; Fire Punch; Flamethrower
127,Pinsir,Bug,130,184,186,24,9,—,Fury Cutter; Rock Smash,Submission; Vice Grip; X-Scissor
128,Tauros,Normal,150,148,184,24,9,—,Tackle; Zen Headbutt,Earthquake; Horn Attack; Iron Head
129,Magikarp,Water,40,42,84,56,15,400,Splash,Struggle
130,Gyarados,Water; Flying,190,192,196,8,7,—,Bite; Dragon Breath,Dragon Pulse; Hydro Pump; Twister
131,Lapras,Water; Ice,260,186,190,16,9,—,Frost Breath; Ice Shard,Blizzard; Dragon Pulse; Ice Beam
132,Ditto,Normal,96,110,110,16,10,—,Pound,Struggle
133,Eevee,Normal,110,114,128,32,10,25,Quick Attack; Tackle,Body Slam; Dig; Swift
134,Vaporeon,Water,260,186,168,12,6,—,Water Gun,Aqua Tail; Hydro Pump; Water Pulse
135,Jolteon,Electric,130,192,174,12,6,—,Thunder Shock,Discharge; Thunder; Thunderbolt
136,Flareon,Fire,130,238,178,12,6,—,Ember,Fire Blast; Flamethrower; Heat Wave
137,Porygon,Normal,130,156,158,32,9,—,Quick Attack; Tackle,Discharge; Psybeam; Signal Beam
138,Omanyte,Rock; Water,70,132,160,32,9,50,Mud Shot; Water Gun,Ancient Power; Brine; Rock Tomb
139,Omastar,Rock; Water,140,180,202,12,5,—,Rock Throw; Water Gun,Ancient Power; Hydro Pump; Rock Slide
140,Kabuto,Rock; Water,60,148,142,32,9,50,Mud Shot; Scratch,Ancient Power; Aqua Jet; Rock Tomb
141,Kabutops,Rock; Water,120,190,190,12,5,—,Fury Cutter; Mud Shot,Ancient Power; Stone Edge; Water Pulse
142,Aerodactyl,Rock; Flying,160,182,162,16,9,—,Bite; Steel Wing,Ancient Power; Hyper Beam; Iron Head
143,Snorlax,Normal,320,180,180,16,9,—,Lick; Zen Headbutt,Body Slam; Earthquake; Hyper Beam
144,Articuno,Ice; Flying,180,198,242,—,10,—,Frost Breath,Blizzard; Ice Beam; Icy Wind
145,Zapdos,Electric; Flying,180,232,194,—,10,—,Thunder Shock,Discharge; Thunder; Thunderbolt
146,Moltres,Fire; Flying,180,242,194,—,10,—,Ember,Fire Blast; Flamethrower; Heat Wave
147,Dratini,Dragon,82,128,110,32,9,25,Dragon Breath,Aqua Tail; Twister; Wrap
148,Dragonair,Dragon,122,170,152,8,6,100,Dragon Breath,Aqua Tail; Dragon Pulse; Wrap
149,Dragonite,Dragon; Flying,182,250,212,4,5,—,Dragon Breath; Steel Wing,Dragon Claw; Dragon Pulse; Hyper Beam
150,Mewtwo,Psychic,212,284,202,—,10,—,Confusion; Psycho Cut,Hyper Beam; Psychic; Shadow Ball
151,Mew,Psychic,200,220,220,—,10,—,Pound,Dragon Pulse; Earthquake; Fire Blast; Hurricane; Hyper Beam; Moonblast; Psychic; Solar Beam; Thunder";

        #endregion

        protected override void Seed(AzuremonContext context)
        {
            var lines = Cats.Split(new [] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var line in lines)
            {
                var columns = line.Split(',');

                context.Categories.Add(new Category
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = columns[0],
                    Color = columns[1]
                });
            }

            context.SaveChanges();


            var pokes = Pokemon.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);


            var allCats = context.Categories.ToList();

            foreach (var poke in pokes)
            {
                var columns = poke.Split(',');

                var cats = columns[2].Split(';');

                context.Pokemons.Add(new Pokemon
                {
                    Id = Guid.NewGuid().ToString(),
                    Number = int.Parse(columns[0]),
                    Name = columns[1],
                    Categories = allCats.Where(t => cats.Any(x => x.Trim() == t.Name)).ToList(),
                    Stamina = double.Parse(columns[3]),
                    Attack = double.Parse(columns[4]),
                    Defense = double.Parse(columns[5]),
                    CaptureRate = columns[6],
                    FleeRate = columns[7],
                    Candy = columns[8],
                    QuickMoves = columns[9],
                    SpecialMoves = columns[10]
                });

            }

            context.SaveChanges();
        }
    }
}