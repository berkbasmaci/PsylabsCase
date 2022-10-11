using Microsoft.EntityFrameworkCore;
using PsylabsCase.Core.Entities;

namespace PsylabsCase.Data
{
    public class PsylabsDbContext : DbContext
    {
        public PsylabsDbContext(DbContextOptions<PsylabsDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Exam> Exams { get; set; }
        public DbSet<ExamQuestion> ExamQuestions { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<UserExam> UserExams { get; set; }
        public DbSet<ExamQuestionAnswer> ExamQuestionAnswers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ExamQuestion>()
                .HasKey(t => new
                {
                    t.ExamId,  // composite key def
                    t.QuestionId
                });

            modelBuilder.Entity<ExamQuestion>()
                .HasOne(t => t.Exam)
                .WithMany(t => t.ExamQuestions)
                .HasForeignKey(t => t.ExamId);

            modelBuilder.Entity<ExamQuestion>()
                .HasOne(t => t.Question)
                .WithMany(t => t.ExamQuestions)
                .HasForeignKey(t => t.QuestionId);

            /* - - - - - - - - - - - - - - - - - - - - - - - - - */

            modelBuilder.Entity<ExamQuestionAnswer>()
                .HasKey(t => new
                {
                    t.ExamId,  // composite key def
                    t.QuestionId,
                    t.UserId,
                    t.AnswerId,
                });

            /* - - - - - - - - - - - - - - - - - - - - - - - - - */

            modelBuilder.Entity<UserExam>()
                .HasKey(t => new
                {
                    t.ExamId,  // composite key def
                    t.UserId,
                });

            modelBuilder.Entity<UserExam>()
                .HasOne(t => t.User)
                .WithMany(t => t.UserExams)
                .HasForeignKey(t => t.UserId);

            modelBuilder.Entity<UserExam>()
                .HasOne(t => t.Exam)
                .WithMany(t => t.UserExams)
                .HasForeignKey(t => t.ExamId);


            /*-------------------------------------------------------*/

            modelBuilder.Entity<ExamQuestionAnswer>()
                .HasOne(t => t.Question)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction);

            base.OnModelCreating(modelBuilder);
        }
    }
}
