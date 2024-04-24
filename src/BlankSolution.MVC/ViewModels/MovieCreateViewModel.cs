namespace BlankSolution.MVC.ViewModels
{
    public class MovieCreateViewModel
    {
        public string Name { get; set; }
        public string Desc { get; set; }
        public double CostPrice { get; set; }
        public double SalePrice { get; set; }
        public IFormFile ImageFile { get; set; }
    }
}
