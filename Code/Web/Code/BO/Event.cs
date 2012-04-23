using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;

namespace Web.Code.BO
{
    [Serializable]
    public class Event : BaseBO
    {
        public string Name { get; set; }
        public string Year { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Details { get; set; }
        public string Location { get; set; }
        public string Level { get; set; }
        public string State { get; set; }

        public List<Category> Categories { get; set; }
        public List<Weight> Weights { get; set; }
        public List<Resource> Resources { get; set; }
        public List<Result> Results { get; set; }
    }

    [Serializable]
    public class Category : BaseBO, IEquatable<Category>
    {
        public string Name { get; set; }

        public override bool Equals(object obj)
        {
            return Equals(obj as Category);
        }

        public bool Equals(Category other)
        {
            if (other == null)
                return false;
            return this.Name == other.Name;
        }
    }

    [Serializable]
    public class Weight : BaseBO, IEquatable<Weight>
    {
        public string Class { get; set; }

        public override bool Equals(object obj)
        {
            return Equals(obj as Weight);
        }

        public bool Equals(Weight other)
        {
            if (other == null)
                return false;

            return this.Class == other.Class;
        }
    }

    [Serializable]
    public class Result : BaseBO, IEquatable<Result>
    {
        public string Category { get; set; }
        public string Weight { get; set; }
        public string Gold { get; set; }
        public string Silver { get; set; }
        public string Bronze { get; set; }

        public override bool Equals(object obj)
        {
            return Equals(obj as Result);
        }

        public bool Equals(Result other)
        {
            if (other == null)
                return false;

            return this.Category == other.Category && this.Weight == other.Weight;
        }
    }

    [Serializable]
    public class Resource : BaseBO, IEquatable<Resource>
    {
        public string Name { get; set; }
        public string Title { get; set; }
        public string FileName { get; set; }

        public override bool Equals(object obj)
        {
            return Equals(obj as Resource);
        }

        public bool Equals(Resource other)
        {
            if (other == null)
                return false;

            return this.FileName == other.FileName;
        }
    }
}