using System;
namespace OnLineTest.BLL
{
    /// <summary>
    /// SuggestionKeywordManager
    /// </summary>
    public partial class SuggestionKeywordManager
    {
        public bool Exists(string SuggestionKeywords,out int SuggestionKeywordsId)
        {
           return dal.Exists(SuggestionKeywords, out SuggestionKeywordsId);
        }
    }
}

