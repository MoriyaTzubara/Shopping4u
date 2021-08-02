namespace BL.Entities
{
    using System.Collections.Generic;

    public class Output
    {
        #region Public Properties

        public IList<Rule> StrongRules { get; set; }
        public ItemsDictionary FrequentItems { get; set; }
        public Dictionary<string, Dictionary<string, double>> ClosedItemSets { get; set; }
        #endregion
    }
}