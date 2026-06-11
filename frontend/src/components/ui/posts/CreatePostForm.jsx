import { useState } from "react";
import { Input } from "@/components/ui/Input";
import { Button } from "@/components/ui/Button";
import api from "@/services/api.js";

export default function CreatePostForm({ authorId, handleCreate, closeForm }) {
  const [title, setTitle] = useState("");
  const [content, setContent] = useState("");
  const [semester, setSemester] = useState("");

  const onSubmit = async (e) => {
    e.preventDefault();

    if (!title || !content || !semester) {
      alert("Please fill in all fields.");
      return;
    }

    try {
      // Send post to backend
      await api.post("/posts", {
        title,
        content,
        semester: parseInt(semester),
        authorId,
        author: null, // backend will attach full author
      });

      // Fetch updated posts from backend
      await handleCreate();

      // Clear form
      setTitle("");
      setContent("");
      setSemester("");
      closeForm();
    } catch (err) {
      console.error("Failed to create post:", err);
      alert("Failed to create post.");
    }
  };

  const onCancel = () => {
    setTitle("");
    setContent("");
    setSemester("");
    closeForm();
  };

  return (
    <div className="bg-white p-4 rounded shadow mb-6">
      <h2 className="text-xl font-semibold mb-2">Create Post</h2>
      <form onSubmit={onSubmit} className="flex flex-col gap-3">
        <Input
          value={title}
          onChange={(e) => setTitle(e.target.value)}
          placeholder="Title"
          required
        />
        <textarea
          value={content}
          onChange={(e) => setContent(e.target.value)}
          placeholder="Write your experience..."
          rows={4}
          className="w-full p-2 border rounded"
          required
        />
        <select
          value={semester}
          onChange={(e) => setSemester(e.target.value)}
          required
          className="w-full p-2 border rounded"
        >
          <option value="">Select semester</option>
          {[1, 2, 3, 4, 5, 6, 7, 8].map((s) => (
            <option key={s} value={s}>
              Semester {s}
            </option>
          ))}
        </select>
        <div className="flex gap-2 mt-2">
          <Button
            type="submit"
            variant="hero"
            className="flex-1 bg-purple-600 hover:bg-purple-700 text-white"
          >
            Create
          </Button>
          <Button type="button" variant="outline" className="flex-1" onClick={onCancel}>
            Cancel
          </Button>
        </div>
      </form>
    </div>
  );
}
