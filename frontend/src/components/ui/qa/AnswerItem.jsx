import { useState, useEffect } from "react";
import { Button } from "@/components/ui/Button";
import { Trophy } from "lucide-react"; // ✅ Trophy icon
import api from "@/services/api.js";

export default function AnswerItem({ answer, questionId, isAdmin, setQuestions, currentUserId }) {
  const [isUpvoted, setIsUpvoted] = useState(false);
  const [upvotes, setUpvotes] = useState(answer.upvotes);

  // Initialize state based on answer prop
  useEffect(() => {
    setUpvotes(answer.upvotes);
    setIsUpvoted(false); // default false; can enhance with user-specific check later
  }, [answer]);

  const handleUpvote = async () => {
  try {
    const res = await api.post(`/answer/${answer.id}/upvote?userId=${currentUserId}`);
    const updatedAnswer = res.data.answer; // ✅ the new answer object from backend

    // ✅ Use the real upvote count from backend
    setUpvotes(updatedAnswer.upvotes);
    setIsUpvoted(prev => !prev); // toggle the trophy color

    // ✅ Update parent question list with new answer data
    setQuestions(prev =>
      prev.map(q =>
        q.id === questionId
          ? {
              ...q,
              answers: q.answers.map(a =>
                a.id === answer.id ? updatedAnswer : a
              ),
            }
          : q
      )
    );
  } catch (error) {
    console.error("Error upvoting answer:", error);
  }
};

  const handleMarkBest = async () => {
    try {
      await api.put(`/answer/${answer.id}/mark-best`);
      setQuestions(prev =>
        prev.map(q =>
          q.id === questionId
            ? { ...q, answers: q.answers.map(a => ({ ...a, isBest: a.id === answer.id })) }
            : q
        )
      );
    } catch (error) {
      console.error("Error marking best answer:", error);
    }
  };

  return (
    <div className={`p-2 border rounded ${answer.isBest ? "border-green-500 bg-green-50" : ""}`}>
      <p>{answer.content}</p>
      <div className="flex items-center space-x-2 text-sm text-muted-foreground mt-1">
        <span>{answer.author?.fullName}</span>

        {/* Upvote Button with Trophy */}
        <Button size="xs" variant="outline" onClick={handleUpvote} className="flex items-center space-x-1">
          <Trophy className={`h-4 w-4 ${isUpvoted ? "text-yellow-400" : ""}`} />
          <span>{upvotes}</span>
        </Button>

        {/* Mark Best Button */}
        {isAdmin && !answer.isBest && (
          <Button size="xs" variant="secondary" onClick={handleMarkBest}>
            Mark Best
          </Button>
        )}
      </div>
    </div>
  );
}
