import { useState, useEffect } from "react";
import { Input } from "@/components/ui/Input";
import { Button } from "@/components/ui/Button";

export default function EditQuestionForm({ question, handleUpdate, closeForm }) {
  const [title, setTitle] = useState(question.title);
  const [content, setContent] = useState(question.content);

  useEffect(() => {
    setTitle(question.title);
    setContent(question.content);
  }, [question]);

  const onSubmit = (e) => {
    e.preventDefault();
    // Send the full question object to the handler
    handleUpdate({ ...question, title, content });
    closeForm();
  };

  return (
    <div className="bg-white p-4 rounded shadow mb-6">
      <h2 className="text-xl font-semibold mb-2">Edit Question</h2>
      <form onSubmit={onSubmit} className="flex flex-col gap-2">
        <Input value={title} onChange={e => setTitle(e.target.value)} placeholder="Title" required />
        <textarea
          value={content}
          onChange={e => setContent(e.target.value)}
          placeholder="Update your question..."
          rows={4}
          className="w-full p-2 border rounded"
          required
        />
        <div className="flex space-x-2 mt-2">
          <Button type="submit" variant="hero">Update</Button>
          <Button type="button" variant="outline" onClick={closeForm}>Cancel</Button>
        </div>
      </form>
    </div>
  );
}
