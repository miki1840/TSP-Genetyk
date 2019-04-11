using System;
using System.Collections.Generic;
using System.Text;

namespace TSP_Genetyk.Classes
{
    //Kod wygenerowany przez Visual Studio na podstawie pliku XML
    // UWAGA: Wygenerowany kod może wymagać co najmniej programu .NET Framework 4.5 lub .NET Core/Standard 2.0.
    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
    public partial class travellingSalesmanProblemInstance
    {

        private string nameField;

        private string sourceField;

        private string descriptionField;

        private int doublePrecisionField;

        private int ignoredDigitsField;

        private travellingSalesmanProblemInstanceVertexEdge[][] graphField;

        /// <remarks/>
        public string name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
            }
        }

        /// <remarks/>
        public string source
        {
            get
            {
                return this.sourceField;
            }
            set
            {
                this.sourceField = value;
            }
        }

        /// <remarks/>
        public string description
        {
            get
            {
                return this.descriptionField;
            }
            set
            {
                this.descriptionField = value;
            }
        }

        /// <remarks/>
        public int doublePrecision
        {
            get
            {
                return this.doublePrecisionField;
            }
            set
            {
                this.doublePrecisionField = value;
            }
        }

        /// <remarks/>
        public int ignoredDigits
        {
            get
            {
                return this.ignoredDigitsField;
            }
            set
            {
                this.ignoredDigitsField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("vertex", IsNullable = false)]
        [System.Xml.Serialization.XmlArrayItemAttribute("edge", IsNullable = false, NestingLevel = 1)]
        public travellingSalesmanProblemInstanceVertexEdge[][] graph
        {
            get
            {
                return this.graphField;
            }
            set
            {
                this.graphField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class travellingSalesmanProblemInstanceVertexEdge
    {

        private double costField;

        private int valueField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public double cost
        {
            get
            {
                return this.costField;
            }
            set
            {
                this.costField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlTextAttribute()]
        public int Value
        {
            get
            {
                return this.valueField;
            }
            set
            {
                this.valueField = value;
            }
        }
    }


}
