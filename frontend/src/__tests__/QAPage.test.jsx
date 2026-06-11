import { render, screen, fireEvent, waitFor } from "@testing-library/react";
import { describe, it, expect, vi } from "vitest";
import CreateQuestionForm from "../components/ui/qa/CreateQuestionForm";

// Minimal mocks
vi.mock("@/services/api", () => ({
  default: { post: vi.fn() }
}));

vi.mock("@/components/ui/Button", () => ({
  Button: ({ children, onClick }) => <button onClick={onClick}>{children}</button>
}));

vi.mock("@/components/ui/Input", () => ({
  Input: ({ value, onChange, placeholder }) => (
    <input value={value} onChange={onChange} placeholder={placeholder} />
  )
}));

describe("CreateQuestionForm", () => {
  it("creates a question successfully", async () => {
    const mockSetQuestions = vi.fn();
    const mockCloseForm = vi.fn();
    const mockApi = (await import("@/services/api")).default;
    
    mockApi.post.mockResolvedValueOnce({ 
      data: { id: 1, title: "Test", content: "Test content", authorId: 1 } 
    });

    render(<CreateQuestionForm 
      authorId={1} 
      setQuestions={mockSetQuestions} 
      closeForm={mockCloseForm} 
    />);

    // Fill and submit form
    fireEvent.change(screen.getByPlaceholderText("Title"), { 
      target: { value: "Test" } 
    });
    fireEvent.change(screen.getByPlaceholderText("Describe your question..."), { 
      target: { value: "Test content" } 
    });
    fireEvent.click(screen.getByText("Submit"));

    // Verify API call and form closure
    await waitFor(() => {
      expect(mockApi.post).toHaveBeenCalledWith("/question", {
        title: "Test",
        content: "Test content", 
        authorId: 1
      });
      expect(mockSetQuestions).toHaveBeenCalled();
      expect(mockCloseForm).toHaveBeenCalled();
    });
  });

  it("closes form when cancel is clicked", () => {
    const mockCloseForm = vi.fn();

    render(<CreateQuestionForm 
      authorId={1} 
      setQuestions={vi.fn()} 
      closeForm={mockCloseForm} 
    />);

    fireEvent.click(screen.getByText("Cancel"));
    expect(mockCloseForm).toHaveBeenCalled();
  });
});