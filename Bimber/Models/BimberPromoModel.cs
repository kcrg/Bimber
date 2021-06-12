using Xamarin.Forms;

namespace Bimber.Models
{
    public class BimberPromoModel
    {
        public int Id { get; set; }
        public string? Glyph { get; set; } = "&#xECAD;";
        public Color? PromoColor { get; set; } = Color.Pink;
        public string? Title { get; set; } = "Binder Platinum";
        public string? Description { get; set; } = "Odkryj zupełnie nowy potencjał Bimbera";
    }
}
