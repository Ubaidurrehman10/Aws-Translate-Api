using Amazon.Translate.Model;
using Amazon.Translate;
using Aws_translation_dahabay.Models;
using Microsoft.AspNetCore.Mvc;
using M = Microsoft.AspNetCore.Mvc;
using Amazon;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting.Server;

namespace Aws_translation_dahabay.Controller
{
    public class TranslateController : M.Controller
    {
        public TranslateController()
        {
        }
        public IActionResult Index()
        {
            return View();
        }
        public ActionResult AddComment()
        {
            var model = new TranslateCommentViewModel();
            return View(model);
        }
        [HttpPost]
        public async Task<ActionResult> TranslateComment(TranslateCommentViewModel comment)
        {
            String awsAccessKeyId = "";
            String awsSecretAccessKeyId = "";
            var translate = new AmazonTranslateClient(awsAccessKeyId, awsSecretAccessKeyId, RegionEndpoint.USEast1);
            var request = new TranslateTextRequest() { Text = comment.CommentText, SourceLanguageCode = "en", TargetLanguageCode = comment.TargetLanguage };
            var model = new TranslatedCommentViewModel()
            {
                CommentText = comment.CommentText,
                SubmitterName = comment.SubmitterName,
                TargetLangauge = comment.TargetLanguage,
                TranslateResponse = await translate.TranslateTextAsync(request) // Make the actual call to Amazon Translate
            };
            return View(model);
        }
    }
}
