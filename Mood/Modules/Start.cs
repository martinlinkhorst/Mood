using Nancy;

namespace Mood.Modules
{
    public class Start : NancyModule
    {
        public Start()
        {
            Get["/"] = parameters =>
                {
                    return View["index.html"];
                };

            Get["/stats"] = parameters =>
                {
                    return View["stats.html"];
                };

            Put["/mood/{status}"] = parameters =>
                {
                    Mood.Save(parameters["status"]);
                    return true;
                };

            Get["/mood/statstoday/"] = parameters =>
                {
                    return Response.AsJson(Mood.GetTodays());
                };

            Get["/mood/stats/{date}"] = parameters =>
                {
                    var date = parameters["date"];
                    return FormatterExtensions.AsJson(Response, Mood.GetByDate(date));
                };
        }
    }
}
