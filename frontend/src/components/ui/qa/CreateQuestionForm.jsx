import { useState } from "react";
import { Input } from "@/components/ui/Input";
import { Button } from "@/components/ui/Button";
import api from "@/services/api.js";

export default function CreateQuestionForm({ authorId, setQuestions, closeForm }) {
  const [title, setTitle] = useState("");
  const [content, setContent] = useState("");

  const handleSubmit = async e => {
    e.preventDefault();
    try {
      const res = await api.post("/question", { title, content, authorId });
      setQuestions(prev => [res.data, ...prev]);
      closeForm();
    } catch (error) {
      console.error("Error creating question:", error);
    }
  };

  return (
    <div className="bg-white p-4 rounded shadow mb-6">
      <h2 className="text-xl font-semibold mb-2">Ask a Question</h2>
      <form onSubmit={handleSubmit} className="flex flex-col gap-2">
        <Input value={title} onChange={e => setTitle(e.target.value)} placeholder="Title" required />
        <textarea
          value={content}
          onChange={e => setContent(e.target.value)}
          placeholder="Describe your question..."
          rows={4}
          className="w-full p-2 border rounded"
          required
        />
        <div className="flex space-x-2 mt-2">
          <Button type="submit" variant="hero">Submit</Button>
          <Button type="button" variant="outline" onClick={closeForm}>Cancel</Button>
        </div>
      </form>
    </div>
  );
}
