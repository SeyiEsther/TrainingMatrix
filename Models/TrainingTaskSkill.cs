namespace TrainingMatrixApp.Models
{
    public class TrainingTaskSkill
    {
        public int TrainingTaskId { get; set; }
        public TrainingTask TrainingTask { get; set; }

        public int SkillId { get; set; }
        public Skill Skill { get; set; }
    }
}