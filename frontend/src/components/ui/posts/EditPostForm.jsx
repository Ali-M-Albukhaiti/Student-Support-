import { useState, useEffect } from "react";
import { Input } from "@/components/ui/Input";
import { Select, SelectTrigger, SelectValue, SelectContent, SelectItem } from "@/components/ui/Select";
import { Button } from "@/components/ui/Button";

export default function EditPostForm({ post, handleUpdate, closeForm }) {
  const [title, setTitle] = useState("");
  const [content, setContent] = useState("");
  const [semester, setSemester] = useState("1");

  // Prefill form when post changes
  useEffect(() => {
    if (post) {
      setTitle(post.title || "");
      setContent(post.content || "");
      setSemester(post.semester ? post.semester.toString() : "1"); // convert number to string
    }
  }, [post]);

   const onSubmit = (e) => {
    e.preventDefault();
     handleUpdate({
      id: post.id,          
      title,
      content,
      semester: parseInt(semester),
      authorId: post.authorId || post.author?.id, // ensure authorId is sent
      author: null          
  });
  closeForm();
};


  const onCancel = () => {
    closeForm();
  };

  return (
    <div className="bg-white p-4 rounded shadow mb-6">
      <h2 className="text-xl font-semibold mb-2">Edit Post</h2>
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
        <div className="flex gap-2 mt-2">
          <Button type="submit" variant="hero" className="flex-1 bg-purple-600 hover:bg-purple-700 text-white">
            Update
          </Button>
          <Button type="button" variant="outline" onClick={onCancel} className="flex-1">
            Cancel
          </Button>
        </div>
      </form>
    </div>
  );
}
