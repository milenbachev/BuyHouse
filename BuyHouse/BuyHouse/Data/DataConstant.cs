namespace BuyHouse.Data
{
    public class DataConstant
    {
        public class Agent 
        {
            public const int NameMinLength = 3;
            public const int NameMaxLength = 40;
            public const int PhoneNumberMinLength = 9;
            public const int PhoneNumberMaxLength = 20;
            public const int DescriptionMinLength = 10;
            public const int DescriptionMaxLength = 200;
        }

        public class Category 
        {
            public const int NameMinLength = 2;
            public const int NameMaxLength = 30;
        }

        public class City 
        {
            public const int NameMinLength = 2;
            public const int NameMaxLength = 30;
        }

        public class Construction 
        {
            public const int NameMinLength = 2;
            public const int NameMaxLength = 30;
        }

        public class Property 
        {
            public const int AreaMinValue = 2;
            public const int AreaMaxValue = int.MaxValue;
            public const int PriceMinValue = 1;
            public const int PriceMaxValue = int.MaxValue;
            public const int DescriptionMinLength = 5;
            public const int DescriptionMaxLength = 200;
        }

        public class TypeOfTransaction 
        {
            public const int NameMinLength = 5;
            public const int NameMaxLength = 30;
        }
    }
}
