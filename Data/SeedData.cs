using HogwartsWebsite.Models;

namespace HogwartsWebsite.Data;

// Populates tables on first run if they are empty
public static class SeedData
{
    public static void Initialize(AppDbContext db)
    {
        SeedAnnouncements(db);
        SeedEvents(db);
        SeedStaff(db);
        SeedCourses(db);
    }

    private static void SeedAnnouncements(AppDbContext db)
    {
        if (db.Announcements.Any()) return;

        db.Announcements.AddRange(
            new Announcement
            {
                Title = "O.W.L. Examination Schedule Released",
                Body = "The Wizarding Examinations Authority has confirmed the dates for this year's Ordinary Wizarding Level examinations. Written papers will be held in the Great Hall from 9 June to 13 June 2026. Practical examinations follow the week after, from 16 June to 20 June 2026. Fifth-year students are advised to collect their individual timetables from Professor McGonagall's office no later than 8 May 2026. Revision sessions will be offered by each subject head during the fortnight prior to examinations.",
                Category = "Academic",
                PublishedDate = new DateTime(2026, 5, 1)
            },
            new Announcement
            {
                Title = "Quidditch Season Opening Match This Saturday",
                Body = "The inter-house Quidditch season opens this Saturday with the first match of the academic year: Gryffindor vs. Slytherin. Kick-off is at 11:00 on the Quidditch Pitch. All students are welcome to attend and cheer for their house. Madam Hooch will referee. Students are reminded that the use of Bludger-enhancing jinxes from the stands is strictly forbidden and will be reported to the relevant Head of House.",
                Category = "Sports",
                PublishedDate = new DateTime(2026, 4, 28)
            },
            new Announcement
            {
                Title = "Third Floor Corridor Temporarily Closed",
                Body = "The Third Floor Corridor on the right-hand side has been closed until further notice for essential maintenance. Students are instructed not to enter this area under any circumstances. The corridor will reopen once work is complete and the area has been inspected and declared safe by the Head of Caretaking. Students found in the corridor during the closure will face immediate disciplinary measures.",
                Category = "Safety",
                PublishedDate = new DateTime(2026, 4, 25)
            },
            new Announcement
            {
                Title = "Duelling Club Now Accepting Applications",
                Body = "The Duelling Club will resume activities this term under the supervision of the Defence Against the Dark Arts department. The club is open to students in Year 3 and above. Meetings will be held every other Saturday evening in the Great Hall. Students wishing to participate must submit their name to their Head of House by 30 April 2026. Basic proficiency in Shield Charms is recommended before joining.",
                Category = "Events",
                PublishedDate = new DateTime(2026, 4, 20)
            },
            new Announcement
            {
                Title = "New Defence Against the Dark Arts Professor Appointed",
                Body = "The Headmaster is pleased to announce the appointment of a new Professor of Defence Against the Dark Arts for the current academic year. The incoming professor brings considerable practical experience and will bring a structured, theory-based approach to the curriculum. An introductory assembly for all Defence Against the Dark Arts classes will be held in the first week of May. Students should continue to attend all scheduled classes as normal.",
                Category = "Academic",
                PublishedDate = new DateTime(2026, 4, 15)
            },
            new Announcement
            {
                Title = "Hogsmeade Visit Dates Confirmed",
                Body = "The next scheduled Hogsmeade weekend will take place on 31 October 2026. Third-year students and above are reminded that they must have a signed permission form on file with their Head of House before they may leave school grounds. Permission forms are available from the school office. Students who have not yet submitted forms should do so by 24 October 2026. Students will depart from the Entrance Hall at 10:00 and must return no later than 17:30.",
                Category = "Events",
                PublishedDate = new DateTime(2026, 4, 8)
            },
            // Archive announcements
            new Announcement
            {
                Title = "House Cup Standings Updated After Easter Break",
                Body = "The House Cup points tally has been updated following the Easter holiday period. Points awarded and deducted during revision sessions and unsupervised common room periods have been tallied by the relevant Heads of House. The updated standings are posted on each house noticeboard. Students are reminded that points may still be gained or lost before the End of Year Feast.",
                Category = "Academic",
                PublishedDate = new DateTime(2026, 4, 1)
            },
            new Announcement
            {
                Title = "Mandatory Safety Briefing for All Years",
                Body = "All students are required to attend a mandatory safety briefing to be held in the Great Hall on the first Thursday back from Easter break. The briefing will cover emergency procedures, restricted areas, and protocols when encountering potentially dangerous magical creatures or artefacts on school grounds. Attendance will be taken. Students who cannot attend due to illness must arrange an alternative session with their Head of House.",
                Category = "Safety",
                PublishedDate = new DateTime(2026, 3, 28)
            },
            new Announcement
            {
                Title = "Library Extended Hours During Examination Revision Period",
                Body = "The Hogwarts Library will offer extended opening hours from 1 April through to the end of the O.W.L. examination period. The library will remain open until 22:00 on weekdays and 20:00 on weekends. Madam Pince asks that all students maintain strict silence in the reading rooms and handle all volumes with care. No food or drink is permitted inside the library at any time.",
                Category = "Academic",
                PublishedDate = new DateTime(2026, 3, 15)
            },
            new Announcement
            {
                Title = "Potions Classroom 3 Temporarily Out of Service",
                Body = "Potions Classroom 3 in the dungeons is temporarily closed following an incident during a sixth-year practical session. Students assigned to Classroom 3 will be relocated to Classroom 1 for the duration of repairs. Professor Snape has adjusted the timetable accordingly; students should check their updated schedules posted outside the Potions department. The classroom is expected to reopen within two weeks.",
                Category = "Safety",
                PublishedDate = new DateTime(2026, 2, 20)
            },
            new Announcement
            {
                Title = "Valentine's Day Feast — Seating Arrangements",
                Body = "Seating for the Valentine's Day Feast will be arranged by year group rather than house. Students are asked to report to the Great Hall at 18:30. Owl-delivered flowers and cards will be distributed during the starter course. Students wishing to send owl post for Valentine's Day should do so by 12 February to ensure timely delivery. The feast concludes at 21:00; all students must return to their common rooms by 21:30.",
                Category = "Events",
                PublishedDate = new DateTime(2026, 2, 10)
            },
            new Announcement
            {
                Title = "Christmas Feast — Attendance Confirmation Required",
                Body = "Students intending to remain at Hogwarts over the Christmas holiday are asked to confirm their attendance at the Christmas Feast by 15 December. Please notify your Head of House in writing. Students returning home for the holiday should ensure they are packed and ready to board the Hogwarts Express on the morning of 20 December. The train departs from Hogsmeade station at 10:00 and arrives at King's Cross at approximately 16:30.",
                Category = "Events",
                PublishedDate = new DateTime(2025, 12, 10)
            },
            new Announcement
            {
                Title = "Duelling Club — Sign-Ups Now Open",
                Body = "The Duelling Club is accepting applications for the autumn term. The club is open to students in Year 3 and above. Sessions focus on controlled, supervised spell practice and are designed to build confidence in defensive magic. Meetings are held fortnightly on Saturday evenings in the Great Hall. Students should submit their name and year to their Head of House by the end of October.",
                Category = "Events",
                PublishedDate = new DateTime(2025, 10, 14)
            },
            new Announcement
            {
                Title = "Welcome Back — Sorting Ceremony Notice",
                Body = "The Sorting Ceremony will take place on 1 September in the Great Hall following the arrival of all students aboard the Hogwarts Express. Returning students are reminded to change into their robes before leaving the train. First-year students will be escorted from the station to the castle by the Keeper of Keys and Grounds. The Opening Feast begins immediately after the Sorting. All students are expected to be present.",
                Category = "Academic",
                PublishedDate = new DateTime(2025, 9, 1)
            }
        );

        db.SaveChanges();
    }

    private static void SeedEvents(AppDbContext db)
    {
        if (db.Events.Any()) return;

        db.Events.AddRange(
            new SchoolEvent
            {
                Title = "Sorting Ceremony & Opening Feast",
                Description = "First-year students are sorted into their houses by the Sorting Hat, followed by the Opening Feast for all students. The Headmaster addresses the school and term officially begins. Students arriving by Hogwarts Express depart from Platform 9¾ at King's Cross Station at 11:00.",
                Location = "The Great Hall",
                EventDate = new DateTime(2025, 9, 1)
            },
            new SchoolEvent
            {
                Title = "Quidditch Season Opener: Gryffindor vs. Slytherin",
                Description = "The first Quidditch match of the season. All students are welcome to attend and cheer for their house. Kick-off is at 11:00. Madam Hooch officiates. Weather charms will be applied to the spectator stands; students are nonetheless advised to dress warmly.",
                Location = "Quidditch Pitch",
                EventDate = new DateTime(2025, 10, 5)
            },
            new SchoolEvent
            {
                Title = "Halloween Feast",
                Description = "The annual Halloween Feast is held in the Great Hall. The hall is decorated with hundreds of live bats, floating jack-o'-lanterns, and enchanted candles. A special menu is prepared by the Hogwarts house-elves. All students are expected to attend.",
                Location = "The Great Hall",
                EventDate = new DateTime(2025, 10, 31)
            },
            new SchoolEvent
            {
                Title = "Christmas Feast",
                Description = "Students remaining at Hogwarts over the Christmas holiday are invited to the Christmas Feast in the Great Hall. The hall is traditionally decorated with twelve enormous Christmas trees and enchanted snow falls from the ceiling. A smaller sitting than usual, as most students travel home for the holidays.",
                Location = "The Great Hall",
                EventDate = new DateTime(2025, 12, 25)
            },
            new SchoolEvent
            {
                Title = "Valentine's Day Feast",
                Description = "A special feast is held on Valentine's Day each year. Students may purchase cards and small gifts from the school office in the days leading up to the event. Owl post deliveries of flowers are accepted for the occasion. The Great Hall will be decorated with pink and gold arrangements.",
                Location = "The Great Hall",
                EventDate = new DateTime(2026, 2, 14)
            },
            new SchoolEvent
            {
                Title = "O.W.L. Written Examinations Begin",
                Description = "Fifth-year O.W.L. examinations commence. Written papers run from 9 June to 13 June, with practical examinations the following week from 16 June to 20 June. The examinations are conducted by the Wizarding Examinations Authority. Strict silence is observed throughout the examination period.",
                Location = "Examination Hall — Great Hall",
                EventDate = new DateTime(2026, 6, 9)
            },
            new SchoolEvent
            {
                Title = "End of Year Feast & House Cup Presentation",
                Description = "The final feast of the academic year, at which the House Cup is awarded to the house with the highest points total. The Great Hall is decorated in the winning house's colours. Students depart the following morning aboard the Hogwarts Express, arriving at King's Cross Station at approximately 18:00.",
                Location = "The Great Hall",
                EventDate = new DateTime(2026, 6, 30)
            }
        );

        db.SaveChanges();
    }

    private static void SeedStaff(AppDbContext db)
    {
        if (db.Staff.Any()) return;

        db.Staff.AddRange(
            new StaffMember
            {
                FullName = "Albus Percival Wulfric Brian Dumbledore",
                Title = "Professor",
                Subject = null,
                Bio = "Widely considered the greatest wizard of his age, Albus Dumbledore served as Headmaster of Hogwarts for several decades following his tenure as Transfiguration professor. A former student of Hogwarts himself, he is celebrated for his defeat of the Dark wizard Grindelwald in 1945, his twelve uses of dragon's blood, and his work on alchemy with Nicolas Flamel. He is also a member of the International Confederation of Wizards and a former Supreme Mugwump of the International Confederation.",
                PhotoPath = "Albus-Dumbledore.jpg",
                IsHeadmaster = true
            },
            new StaffMember
            {
                FullName = "Minerva McGonagall",
                Title = "Professor",
                Subject = "Transfiguration & Deputy Headmistress",
                Bio = "An Animagus who can transform into a tabby cat, Professor McGonagall is one of the most accomplished Transfiguration practitioners in the world. She is also Head of Gryffindor House and Deputy Headmistress, known for her strict but fair approach to discipline.",
                PhotoPath = "Minerva-McGonagall.jpg",
                IsHeadmaster = false
            },
            new StaffMember
            {
                FullName = "Severus Snape",
                Title = "Professor",
                Subject = "Potions",
                Bio = "Head of Slytherin House and a potions master of exceptional skill, Professor Snape is the author of many advances in the field. A former student of Hogwarts himself, he joined the teaching staff shortly after completing his studies and has since become the school's most demanding instructor.",
                PhotoPath = "Severus-Snape.webp",
                IsHeadmaster = false
            },
            new StaffMember
            {
                FullName = "Filius Flitwick",
                Title = "Professor",
                Subject = "Charms",
                Bio = "Head of Ravenclaw House, Professor Flitwick is a former duelling champion and one of the most skilled Charms practitioners in Britain. Despite his diminutive stature, his magical ability and enthusiasm for the subject make him one of the most popular teachers at Hogwarts.",
                PhotoPath = "Filius-flitwick.webp",
                IsHeadmaster = false
            },
            new StaffMember
            {
                FullName = "Pomona Sprout",
                Title = "Professor",
                Subject = "Herbology",
                Bio = "Head of Hufflepuff House, Professor Sprout runs the school greenhouses with boundless energy and expertise. She is the foremost authority on magical plants at Hogwarts and has contributed significantly to the school's knowledge of Mandrakes and Devil's Snare, among many other specimens.",
                PhotoPath = "Pomona-Sprout.webp",
                IsHeadmaster = false
            },
            new StaffMember
            {
                FullName = "Rubeus Hagrid",
                Title = "Professor",
                Subject = "Care of Magical Creatures",
                Bio = "Hogwarts' gamekeeper for many years before taking on the Care of Magical Creatures post, Hagrid is deeply knowledgeable about magical beasts of all kinds. His enthusiasm sometimes leads him to underestimate the danger of certain creatures, but his love for them is entirely genuine.",
                PhotoPath = "Rubeus-Hagrid.jpg",
                IsHeadmaster = false
            },
            new StaffMember
            {
                FullName = "Sybill Trelawney",
                Title = "Professor",
                Subject = "Divination",
                Bio = "A great-great-granddaughter of the celebrated Seer Cassandra Trelawney, Professor Trelawney teaches Divination from the North Tower. Her teaching style is atmospheric and intense, though her predictions are viewed with varying degrees of scepticism by students and staff alike.",
                PhotoPath = "Sybill-Trelawney.webp",
                IsHeadmaster = false
            },
            new StaffMember
            {
                FullName = "Cuthbert Binns",
                Title = "Professor",
                Subject = "History of Magic",
                Bio = "The only ghost on the Hogwarts teaching staff, Professor Binns is believed to have simply got up to teach one day and left his body behind. His lectures are extraordinarily detailed but delivered in a monotone that most students find difficult to stay awake through.",
                PhotoPath = null,
                IsHeadmaster = false
            },
            new StaffMember
            {
                FullName = "Septima Vector",
                Title = "Professor",
                Subject = "Arithmancy",
                Bio = "Professor Vector teaches Arithmancy, the study of the magical properties of numbers and their use in predicting the future. The subject is considered highly demanding and is taught only at O.W.L. level and above. Her classes are popular with ambitious students seeking to diversify their magical skills.",
                PhotoPath = null,
                IsHeadmaster = false
            },
            new StaffMember
            {
                FullName = "Rolanda Hooch",
                Title = "Madam",
                Subject = "Flying & Quidditch Referee",
                Bio = "Madam Hooch teaches first-year students to fly and serves as the official referee for all Hogwarts Quidditch matches. She has hawk-like yellow eyes and a broom she can manoeuvre with extraordinary precision. She trained at the Quidditch Academy of Excellence in her youth.",
                PhotoPath = "madam-hooch.jpg",
                IsHeadmaster = false
            },
            new StaffMember
            {
                FullName = "Galatea Merrythought",
                Title = "Professor",
                Subject = "Defence Against the Dark Arts",
                Bio = "Professor Merrythought brings decades of practical experience to the Defence Against the Dark Arts classroom. A former Auror, she is known for her rigorous and structured approach to the curriculum, emphasising both theoretical understanding and practical spell-work. She returned to Hogwarts after a period of service with the Ministry of Magic.",
                PhotoPath = null,
                IsHeadmaster = false
            }
        );

        db.SaveChanges();
    }

    private static void SeedCourses(AppDbContext db)
    {
        if (db.Courses.Any()) return;

        db.Courses.AddRange(
            // Core
            new Course { Name = "Astronomy", Description = "Students study the night sky, celestial bodies, and their movements. Practical observations are conducted from the Astronomy Tower at midnight.", YearLevels = "Years 1–7", Category = "Core" },
            new Course { Name = "Charms", Description = "The study of spells that add properties to an object or creature. Core charms include Wingardium Leviosa, Accio, and Lumos.", YearLevels = "Years 1–7", Category = "Core" },
            new Course { Name = "Defence Against the Dark Arts", Description = "Students learn to recognise, counter, and defend themselves against Dark creatures, forces, and spells.", YearLevels = "Years 1–7", Category = "Core" },
            new Course { Name = "Herbology", Description = "The study and care of magical plants and fungi, including their properties and uses in Potions and other branches of magic.", YearLevels = "Years 1–7", Category = "Core" },
            new Course { Name = "History of Magic", Description = "A survey of significant magical events and figures throughout history. One of the few subjects taught by a ghost, Professor Binns.", YearLevels = "Years 1–7", Category = "Core" },
            new Course { Name = "Potions", Description = "The preparation of magical potions using a precise combination of ingredients. Students brew everything from simple remedies to complex draughts.", YearLevels = "Years 1–7", Category = "Core" },
            new Course { Name = "Transfiguration", Description = "The science and art of changing the form or appearance of an object. Considered one of the most complex branches of magic.", YearLevels = "Years 1–7", Category = "Core" },
            new Course { Name = "Flying", Description = "Instruction in broomstick flight. Mandatory for first-year students; forms the foundation for Quidditch participation.", YearLevels = "Year 1", Category = "Core" },

            // Elective
            new Course { Name = "Arithmancy", Description = "The study of the magical properties of numbers and numerical divination. A demanding elective favoured by analytically minded students.", YearLevels = "Years 3–7", Category = "Elective" },
            new Course { Name = "Care of Magical Creatures", Description = "Hands-on study of magical beasts, their habitats, feeding, and care. Classes are held on the Hogwarts grounds.", YearLevels = "Years 3–7", Category = "Elective" },
            new Course { Name = "Divination", Description = "The art of predicting the future through various methods including tea-leaf reading, crystal balls, and dream interpretation.", YearLevels = "Years 3–7", Category = "Elective" },
            new Course { Name = "Muggle Studies", Description = "An examination of the non-magical world from a wizarding perspective — Muggle technology, culture, and daily life.", YearLevels = "Years 3–7", Category = "Elective" },
            new Course { Name = "Study of Ancient Runes", Description = "The study of runic script and its magical applications. Involves translation of ancient texts and understanding runic enchantments.", YearLevels = "Years 3–7", Category = "Elective" },

            // Advanced Elective
            new Course { Name = "Advanced Arithmancy Studies", Description = "An extension of O.W.L.-level Arithmancy for students pursuing N.E.W.T. qualifications. Covers complex numerical magical theory.", YearLevels = "Years 6–7", Category = "Advanced Elective" },
            new Course { Name = "Advanced Rune Translation", Description = "N.E.W.T.-level study of ancient runic systems and their application in modern spell construction and warding.", YearLevels = "Years 6–7", Category = "Advanced Elective" },
            new Course { Name = "Alchemy", Description = "The ancient study of transforming base metals and substances. Only offered when sufficient student interest exists at N.E.W.T. level.", YearLevels = "Years 6–7", Category = "Advanced Elective" },

            // Other
            new Course { Name = "Apparition", Description = "Instruction in the magical ability to teleport from one location to another. Available to students aged seventeen and above. Taught in short-term courses.", YearLevels = "Year 6 (age 17+)", Category = "Other" },
            new Course { Name = "Ghoul Studies", Description = "Examination of ghoul behaviour, habitats, and interaction with wizarding households. An elective offered only when a qualified instructor is available.", YearLevels = "Years 3–5", Category = "Other" },
            new Course { Name = "Xylomancy", Description = "Divination using twigs and the interpretation of fallen branches. An occasional offering within the Divination department.", YearLevels = "Years 3–5", Category = "Other" },

            // Extra-Curricular
            new Course { Name = "Quidditch", Description = "Inter-house Quidditch competition and team training. Students must try out for their house team. Matches are officiated by Madam Hooch.", YearLevels = "Years 1–7", Category = "Extra-Curricular" },
            new Course { Name = "Duelling Club", Description = "Supervised practice in defensive and offensive spell-casting. Open to students in Year 3 and above. Meets fortnightly in the Great Hall.", YearLevels = "Years 3–7", Category = "Extra-Curricular" },
            new Course { Name = "Gobstones Club", Description = "Competitive Gobstones play. Open to all year groups. Meetings are held in the Hogwarts grounds on weekends.", YearLevels = "Years 1–7", Category = "Extra-Curricular" },
            new Course { Name = "Wizard Chess Club", Description = "Strategic wizard chess competition. Open to all years. Regular inter-house tournaments are organised by the club captain.", YearLevels = "Years 1–7", Category = "Extra-Curricular" },
            new Course { Name = "Frog Choir", Description = "A vocal ensemble accompanied by trained Frog Instruments. Open to all students with an interest in magical music.", YearLevels = "Years 1–7", Category = "Extra-Curricular" },
            new Course { Name = "Hogwarts Orchestra", Description = "An instrumental ensemble open to students with prior musical ability. Performs at school feasts and special events.", YearLevels = "Years 1–7", Category = "Extra-Curricular" }
        );

        db.SaveChanges();
    }
}
