using System.Collections.Generic;

namespace MFluentBuilderExtensionsTests.TestData
{
    internal class Sample
    {
        private int privateIntField;
        public int ValueOfPrivateIntField => privateIntField;
        private string privateStringField;
        public string ValueOfPrivateStringField => privateStringField;

        public string StringProperty { get; internal set; }
        public int IntProperty { get; internal set; }
        public bool BoolProperty { get; internal set; }

        private List<string> privateListOfStrings;
        public List<string> ValueOfPrivateListOfStrings => privateListOfStrings;

        public List<string> PublicListOfStrings { get; internal set; }

        public SubSample InstanceOfSubSample { get; internal set; }

        private SubSample privateInstanceOfSubSample;
        public SubSample ValueOfPrivateInstanceOfSubSample => privateInstanceOfSubSample;

        public List<SubSample> PublicListOfInstanceOfSubSample { get; internal set; }

        private List<SubSample> privateListOfInstanceOfSubSample;
        public List<SubSample> ValueOfPrivateListOfInstanceOfSubSample => privateListOfInstanceOfSubSample;

        public PublicImpliciteSample InstanceOfPublicImpliciteSample { get; internal set; }

        private PrivateImpliciteSample privateInstanceOfPrivateImpliciteSample;
        public PrivateImpliciteSample ValueOfInstanceOfPrivateImpliciteSample => privateInstanceOfPrivateImpliciteSample;

        public decimal DecimalProperty { get; internal set; }

        private char privateCharField;
        public char ValueOfPrivateCharField => privateCharField;

        public List<decimal> PublicListOfDecimal { get; internal set; }

        private List<char> privateListOfChar;
        public List<char> ValueOfPrivateListOfChar => privateListOfChar;

        public List<PublicImpliciteSample> ListOfInstanceOfPublicImpliciteSample { get; internal set; }

        private List<PrivateImpliciteSample> listOfInstanceOfPrivateImpliciteSample;
        public List<PrivateImpliciteSample> ValueOfListOfInstanceOfPrivateImpliciteSample => listOfInstanceOfPrivateImpliciteSample;
    }
}