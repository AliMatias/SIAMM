CREATE TABLE "quiz_question"(
	"id" INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE,
	"question" TEXT NOT NULL,
	"is_choice" BOOLEAN NOT NULL	
	);