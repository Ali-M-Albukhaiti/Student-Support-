import { useEffect, useState } from "react";
import api from "@/services/api.js";
import { Button } from "@/components/ui/Button";
import { Plus, User, Calendar } from "lucide-react";

import CreateQuestionForm from "@/components/ui/qa/CreateQuestionForm";
import EditQuestionForm from "@/components/ui/qa/EditQuestionForm";
import AnswerForm from "@/components/ui/qa/AnswerForm";
import AnswerItem from "@/components/ui/qa/AnswerItem";
import SearchFilters from "@/components/ui/posts/SearchFilters";

function QAPage() {
  const [questions, setQuestions] = useState([]);
  const [showCreateForm, setShowCreateForm] = useState(false);
  const [editingQuestion, setEditingQuestion] = useState(null);
  const [searchQuery, setSearchQuery] = useState("");
  const [currentUserId] = useState(1); // logged-in user
  const [isAdmin, setIsAdmin] = useState(false);

  // Filters
  const [studyPrograms, setStudyPrograms] = useState([]);
  const [semesterFilter, setSemesterFilter] = useState("all");
  const [studyProgramFilter, setStudyProgramFilter] = useState("all");

  // Fetch questions
  const fetchQuestions = async () => {
    try {
      const res = await api.get("/question");
      setQuestions(res.data);
      const programs = Array.from(
        new Set(res.data.map(q => q.author?.studyProgram).filter(Boolean))
      );
      setStudyPrograms(programs);
    } catch (error) {
      console.error("Error fetching questions:", error);
    }
  };

  useEffect(() => {
    fetchQuestions();
  }, []);

  // Delete question
  const handleDeleteQuestion = async (questionId) => {
    if (!window.confirm("Are you sure you want to delete this question?")) return;
    try {
      await api.delete(`/question/${questionId}`);
      setQuestions(prev => prev.filter(q => q.id !== questionId));
    } catch (error) {
      console.error("Error deleting question:", error);
      alert("Failed to delete question.");
    }
  };

  // Update question
  const handleUpdateQuestion = async (updatedQuestion) => {
    try {
      await api.put(`/question/${updatedQuestion.id}`, updatedQuestion);
      const res = await api.get("/question");
      setQuestions(res.data);
      setEditingQuestion(null);
    } catch (error) {
      console.error("Error updating question:", error);
      alert("Failed to update question.");
    }
  };

  // Filters
  const filteredQuestions = questions.filter(q => {
    const matchesSearch =
      q.title.toLowerCase().includes(searchQuery.toLowerCase()) ||
      q.content.toLowerCase().includes(searchQuery.toLowerCase());
    const matchesSemester =
      semesterFilter === "all" || q.semester === parseInt(semesterFilter);
    const matchesProgram =
      studyProgramFilter === "all" || q.author?.studyProgram === studyProgramFilter;
    return matchesSearch && matchesSemester && matchesProgram;
  });

  return (
    <div className="min-h-screen bg-muted/20 p-4">
      {/* Header */}
      <div className="flex flex-col lg:flex-row lg:items-center lg:justify-between gap-4 mb-6">
        <div>
          <h1 className="text-3xl font-bold">Q&A</h1>
          <p className="text-muted-foreground">
            Ask technical questions and get answers from fellow students
          </p>
        </div>
        <Button variant="purple" onClick={() => setShowCreateForm(!showCreateForm)}>
          <Plus className="h-4 w-4 mr-2" /> Ask a Question
        </Button>
      </div>

      {/* Create Form */}
      {showCreateForm && (
        <CreateQuestionForm
          authorId={currentUserId}
          closeForm={() => setShowCreateForm(false)}
          setQuestions={setQuestions}
        />
      )}

      {/* Edit Form */}
      {editingQuestion && (
        <EditQuestionForm
          question={editingQuestion}
          setQuestions={setQuestions}
          handleUpdate={handleUpdateQuestion}
          closeForm={() => setEditingQuestion(null)}
        />
      )}

      {/* Search Filters */}
      <SearchFilters
        searchQuery={searchQuery}
        setSearchQuery={setSearchQuery}
        semesterFilter={semesterFilter}
        setSemesterFilter={setSemesterFilter}
        studyProgramFilter={studyProgramFilter}
        setStudyProgramFilter={setStudyProgramFilter}
        studyPrograms={studyPrograms}
      />

      {/* Questions List */}
      <div className="space-y-6">
        {filteredQuestions.length === 0 && <p>No questions found.</p>}
        {filteredQuestions.map(question => (
          <div key={question.id} className="bg-white p-4 rounded shadow">
            <div className="mb-2">
              <h2 className="text-xl font-semibold">{question.title}</h2>
              <p className="text-muted-foreground">{question.content}</p>
              <div className="flex items-center space-x-2 text-sm text-muted-foreground mt-2">
                <User className="h-4 w-4" />
                <span>{question.author?.fullName}</span>
                <Calendar className="h-3 w-3" />
                <span>{new Date(question.createdAt).toLocaleDateString()}</span>
              </div>

              {/* Edit/Delete Buttons */}
              {question.author?.id === currentUserId && (
                <div className="flex space-x-2 mt-2">
                  <Button size="sm" variant="outline" onClick={() => setEditingQuestion(question)}>
                    Edit
                  </Button>
                  <Button size="sm" variant="destructive" onClick={() => handleDeleteQuestion(question.id)}>
                    Delete
                  </Button>
                </div>
              )}
            </div>

            {/* Answers */}
            <div className="mt-4 space-y-2">
              {question.answers?.map(answer => (
                <AnswerItem
                  key={answer.id}
                  answer={answer}
                  questionId={question.id}
                  isAdmin={isAdmin}
                  setQuestions={setQuestions}
                  currentUserId={currentUserId} // ✅ added here
                />
              ))}
            </div>

            {/* Answer Form */}
            <AnswerForm
              questionId={question.id}
              authorId={currentUserId}
              setQuestions={setQuestions}
            />
          </div>
        ))}
      </div>
    </div>
  );
}

export default QAPage;
