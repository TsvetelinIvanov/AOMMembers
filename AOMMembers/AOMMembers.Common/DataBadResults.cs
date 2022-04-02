namespace AOMMembers.Common
{
    public static class DataBadResults
    {
        public const string UnauthorizedEditMessage = "Можете да редактирате само неща които сте създали!";
        public const string UnauthorizedDeleteMessage = "Можете да триете само неща които сте създали!";

        public const string MemberCreateWithoutApplicationUserBadResult = "Firstly, You must have \"ApplicationUser\" and then add \"Member\" to it!";
        public const string MemberCreateWithoutApplicationUserBadRequest = "Трябва първо да сте \"Потребител\" и след това да станете \"Член\"!";
        public const string MemberWithoutApplicationUserBadResult = "You must have \"ApplicationUser\" to do anything with \"Member\"!";
        public const string MemberWithoutApplicationUserBadRequest = "Трябва да сте \"Потребител\" за да редактирате членския профил!";

        public const string CitizenCreateWithoutMemberBadResult = "Firstly, You must have \"Member\" and then add \"Citizen\" to it!";
        public const string CitizenCreateWithoutMemberBadRequest = "Трябва първо да сте \"Член\" и след това да добавите \"Гражданин\" към него!";
        public const string CitizenWithoutMemberBadResult = "You must be \"Member\" to do anything with \"Citizen\"!";
        public const string CitizenWithoutMemberBadRequest = "Трябва да сте \"Член\" за да правите нещо с гражданския профил!";

        public const string EducationCreateWithoutCitizenBadResult = "Firstly, You must have \"Citizen\" and then add \"Education\" to it!";
        public const string EducationCreateWithoutCitizenBadRequest = "Трябва първо да имате \"Гражданин\" и след това да добавите \"Образование\" към него!";
        public const string EducationWithoutCitizenBadResult = "You must have \"Citizen\" to do anything with \"Education\"!";
        public const string EducationWithoutCitizenBadRequest = "Трябва да имате \"Гражданин\" за да правите нещо с образованието!";

        public const string QualificationCreateWithoutEducationBadResult = "Firstly, You must have \"Education\" and then add qualifications to it!";
        public const string QualificationCreateWithoutEducationBadRequest = "Трябва първо да имате \"Образование\" и след това да добавяте квалификации към него!";
        public const string QualificationWithoutEducationBadResult = "You must have \"Education\" to do anything with \"Qualification\"!";
        public const string QualificationWithoutEducationBadRequest = "Трябва да имате \"Образование\" за да правите нещо с квалификацията!";

        public const string CareerCreateWithoutCitizenBadResult = "Firstly, You must have \"Citizen\" and then add \"Career\" to it!";
        public const string CareerCreateWithoutCitizenBadRequest = "Трябва първо да имате \"Гражданин\" и след това да добавите \"Кариера\" към него!";
        public const string CareerWithoutCitizenBadResult = "You must have \"Citizen\" to do anything with \"Career\"!";
        public const string CareerWithoutCitizenBadRequest = "Трябва да имате \"Гражданин\" за да правите нещо с кариерата!";

        public const string WorkPositionCreateWithoutCareerBadResult = "You must firs have \"Career\" and then add \"WorkPosition\" to it!";
        public const string WorkPositionCreateWithoutCareerBadRequest = "Трябва първо да имате \"Кариера\" и след това да добавяте работни постове към нея!";
        public const string WorkPositionWithoutCareerBadResult = "You must have \"Career\" to do anything with \"WorkPosition\"!";
        public const string WorkPositionWithoutCareerBadRequest = "Трябва да имате \"Кариера\" за да правите нещо с работния пост!";

        public const string RelationshipCreateWithoutMemberBadResult = "Firstly, You must have \"Member\" and then add \"Relationship\" to it!";
        public const string RelationshipCreateWithoutMemberBadRequest = "Трябва първо да сте \"Член\" и след това да добавяте връзки към него!";
        public const string RelationshipCreateWithInexistantCitizenBadResult = "The \"Citizen\" is not regitered in this application!";
        public const string RelationshipCreateWithInexistantCitizenBadRequest = "\"Гражданинът\" не е регистриран в приложението!";
        public const string RelationshipWithoutMemberBadResult = "You must have \"Member\" to do anything with \"Relationship\"!";
        public const string RelationshipWithoutMemberBadRequest = "Трябва да сте \"Член\" за да правите нещо с връзките!";

        public const string PartyPositionCreateWithoutMemberBadResult = "Firstly, You must have \"Member\" and then add \"PartyPosition\" to it!";
        public const string PartyPositionCreateWithoutMemberBadRequest = "Трябва първо да сте \"Член\" и след това да добавяте партийни позиции към него!";        
        public const string PartyPositionWithoutMemberBadResult = "You must have \"Member\" to do anything with \"PartyPosition\"!";
        public const string PartyPositionWithoutMemberBadRequest = "Трябва да сте \"Член\" за да правите нещо с партйната позиция!";

        public const string PartyMembershipCreateWithoutCitizenBadResult = "Firstly, You must have \"Citizen\" and then add party memberships to it!";
        public const string PartyMembershipCreateWithoutCitizenBadRequest = "Трябва първо да имате \"Гражданин\" и след това да добавяте партийни членства към него!";
        public const string PartyMembershipWithoutCitizenBadResult = "You must have \"Citizen\" to do anything with \"PartyMembership\"!";
        public const string PartyMembershipWithoutCitizenBadRequest = "Трябва да имате \"Гражданин\" за да правите нещо с партйното членство!";

        public const string MaterialStateCreateWithoutCitizenBadResult = "Firstly, You must have \"Citizen\" and then add \"MaterialState\" to it!";
        public const string MaterialStateCreateWithoutCitizenBadRequest = "Трябва първо да имате \"Гражданин\" и след това да добавите \"Материално състояние\" към него!";
        public const string MaterialStateWithoutCitizenBadResult = "You must have \"Citizen\" to do anything with \"MaterialState\"!";
        public const string MaterialStateWithoutCitizenBadRequest = "Трябва да имате \"Гражданин\" за да правите нещо с материалното състояние!";

        public const string AssetCreateWithoutMaterialStateBadResult = "Firstly, You must have \"MaterialState\" and then add assets to it!";
        public const string AssetCreateWithoutMaterialStateBadRequest = "Трябва първо да имате \"Материално състояние\" и след това да добавяте активи към него!";
        public const string AssetWithoutMaterialStateBadResult = "You must have \"MaterialState\" to do anything with \"Asset\"!";
        public const string AssetWithoutMaterialStateBadRequest = "Трябва да имате \"Материално състояние\" за да правите нещо с актива!";

        public const string PublicImageCreateWithoutMemberBadResult = "Firstly, You must have \"Member\" and then add \"PublicImage\" to it!";
        public const string PublicImageCreateWithoutMemberBadRequest = "Трябва първо да сте \"Член\" и след това да добавите \"Публичен образ\" към него!";
        public const string PublicImageWithoutMemberBadResult = "You must have \"Member\" to do anything with \"PublicImage\"!";
        public const string PublicImageWithoutMemberBadRequest = "Трябва да сте \"Член\" за да правите нещо с ппубличния образ!";

        public const string MediaMaterialCreateWithoutPublicImageBadResult = "Firstly, You must have \"PublicImage\" and then add media materials to it!";
        public const string MediaMaterialCreateWithoutPublicImageBadRequest = "Трябва първо да имате \"Публичен образ\" и след това да добавяте медийни материали към него!";
        public const string MediaMaterialWithoutPublicImageBadResult = "You must have \"PublicImage\" to do anything with \"MediaMaterial\"!";
        public const string MediaMaterialWithoutPublicImageBadRequest = "Трябва да имате \"Публичен образ\" за да правите нещо с медийния материал!";

        public const string LawStateCreateWithoutCitizenBadResult = "Firstly, You must have \"Citizen\" and then add \"LawState\" to it!";
        public const string LawStateCreateWithoutCitizenBadRequest = "Трябва първо да имате \"Гражданин\" и след това да добавите \"Гражданско състояние\" към него!";
        public const string LawStateWithoutCitizenBadResult = "You must have \"Citizen\" to do anything with \"LawState\"!";
        public const string LawStateWithoutCitizenBadRequest = "Трябва да имате \"Гражданин\" за да правите нещо с гражданското състояние!";

        public const string LawProblemCreateWithoutLawStateBadResult = "Firstly, You must have \"LawState\" and then add law problems to it!";
        public const string LawProblemCreateWithoutLawStateBadRequest = "Трябва първо да имате \"Гражданин\" и след това да добавяте проблеми с правосъдието към него!";
        public const string LawProblemWithoutLawStateBadResult = "You must have \"LawState\" to do anything with \"LawProblem\"!";
        public const string LawProblemWithoutLawStateBadRequest = "Трябва да имате \"Гражданско състояние\" за да правите нещо с проблемите с правосъдието!";

        public const string SocietyHelpCreateWithoutCitizenBadResult = "Firstly, You must have \"Citizen\" and then add society helps to it!";
        public const string SocietyHelpCreateWithoutCitizenBadRequest = "Трябва първо да имате \"Гражданин\" и след това да добавяте помощите спрямо обществото към него!";
        public const string SocietyHelpWithoutCitizenBadResult = "You must have \"Citizen\" to do anything with \"SocietyHelp\"!";
        public const string SocietyHelpWithoutCitizenBadRequest = "Трябва да имате \"Гражданин\" за да правите нещо с помощта към обществото!";

        public const string SocietyActivityCreateWithoutCitizenBadResult = "Firstly, You must have \"Citizen\" and then add society activities to it!";
        public const string SocietyActivityCreateWithoutCitizenBadRequest = "Трябва първо да имате \"Гражданин\" и след това да добавяте обществените дейностти към него!";
        public const string SocietyActivityWithoutCitizenBadResult = "You must have \"Citizen\" to do anything with \"SocietyActivity\"!";
        public const string SocietyActivityWithoutCitizenBadRequest = "Трябва да имате \"Гражданин\" за да правите нещо с обществената дейност!";

        public const string WorldviewCreateWithoutCitizenBadResult = "Firstly, You must have \"Citizen\" and then add \"Worldview\" to it!";
        public const string WorldviewCreateWithoutCitizenBadRequest = "Трябва първо да имате \"Гражданин\" и след това да добавите \"Светоглед\" към него!";
        public const string WorldviewWithoutCitizenBadResult = "You must have \"Citizen\" to do anything with \"Worldview\"!";
        public const string WorldviewWithoutCitizenBadRequest = "Трябва да имате \"Гражданин\" за да правите нещо с светогледа!";

        public const string InterestCreateWithoutWorldviewBadResult = "Firstly, You must have \"Worldview\" and then add interests to it!";
        public const string InterestCreateWithoutWorldviewBadRequest = "Трябва първо да имате \"Светоглед\" и след това да добавяте интереси към него!";
        public const string InterestWithoutWorldviewBadResult = "You must have \"Worldview\" to do anything with \"Interest\"!";
        public const string InterestWithoutWorldviewBadRequest = "Трябва да имате \"Светоглед\" за да правите нещо с интереса!";

        public const string SettingCreateWithoutCitizenBadResult = "Firstly, You must have \"Citizen\" and then add settings to it!";
        public const string SettingCreateWithoutCitizenBadRequest = "Трябва първо да имате \"Гражданин\" и след това да добавяте неща към него!";
        public const string SettingWithoutCitizenBadResult = "You must have \"Citizen\" to do anything with \"Setting\"!";
        public const string SettingWithoutCitizenBadRequest = "Трябва да имате \"Гражданин\" за да правите нещо с обекта!";
    }
}
