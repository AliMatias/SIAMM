CREATE TABLE "quiz_answer"(
	"id" INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE,
	"question_id" INTEGER NOT NULL,
	"answer" TEXT NOT NULL,
	"is_correct" BOOLEAN NOT NULL,
	
	CONSTRAINT fk_question
    FOREIGN KEY (question_id)
    REFERENCES quiz_question(id)
	);