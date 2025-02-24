#nullable disable

namespace Diplom;

public partial class Participant
{
	public int PartId { get; set; }
	public int TestId { get; set; }
	public int SpecialId { get; set; }

	public virtual Specialization Special { get; set; }
	public virtual Test1 Test { get; set; }
}