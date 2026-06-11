import { useState } from "react";
import { Button } from "@/components/ui/Button";
import api from "@/services/api.js";

export default function AnswerForm({ questionId, authorId, setQuestions }) {
  const [content, setContent] = useState("");

  const handleSubmit = async e => {
    e.preventDefault();
    try {
      const res = await api.post("/answers", { content, questionId, authorId });
      setQuestions(prev => prev.map(q => 
        q.id === questionId ? { ...q, answers: [...q.answers, res.data] } : q
      ));
      setContent("");
    } catch (error) {
      console.error("Error submitting answer:", error);
    }
  };

  return (
    <form onSubmit={handleSubmit} className="mt-2 flex flex-col gap-2">
      <textarea
        value={content}
        onChange={e => setContent(e.target.value)}
        placeholder="Write your answer..."
        rows={2}
        className="w-full p-2 border rounded"
        required
      />
      <Button type="submit" variant="purple" size="sm">Answer</Button>
    </form>
  );
}
