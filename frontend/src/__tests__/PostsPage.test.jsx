import { render, screen, fireEvent, waitFor } from "@testing-library/react";
import { describe, it, expect, vi } from "vitest";
import PostsPage from "../pages/PostsPage";

// Mock only what's needed
vi.mock("../services/api", () => ({
  default: {
    get: vi.fn(),
    post: vi.fn(),
  }
}));

// Mock only CreatePostForm and SearchFilters
vi.mock("../components/ui/posts/CreatePostForm", () => ({
  default: function CreatePostForm({ closeForm }) {
    return (
      <div data-testid="create-post-form">
        <button onClick={closeForm}>Cancel</button>
        <button data-testid="create-button">Create</button>
      </div>
    );
  }
}));

vi.mock("../components/ui/posts/SearchFilters", () => ({
  default: function SearchFilters({ setSearchQuery }) {
    return (
      <div data-testid="search-filters">
        <input 
          data-testid="search-input" 
          onChange={(e) => setSearchQuery(e.target.value)}
        />
      </div>
    );
  }
}));

// Minimal UI mocks
vi.mock("@/components/ui/Button", () => ({
  Button: ({ children, onClick }) => <button onClick={onClick}>{children}</button>
}));

describe("PostsPage", () => {
  beforeEach(() => {
    vi.clearAllMocks();
  });

  it("shows create form when button is clicked", () => {
    render(<PostsPage />);

    fireEvent.click(screen.getByText("Share Your Experience"));
    expect(screen.getByTestId("create-post-form")).toBeInTheDocument();
  });

  it("filters posts by search", async () => {
    const mockApi = (await import("../services/api")).default;
    mockApi.get.mockResolvedValue({ 
      data: [
        { id: 1, title: "React Post", content: "React content" },
        { id: 2, title: "Vue Post", content: "Vue content" }
      ] 
    });

    render(<PostsPage />);

    await waitFor(() => {
      expect(screen.getByText("React Post")).toBeInTheDocument();
    });

    // Search for "React"
    const searchInput = screen.getByTestId("search-input");
    fireEvent.change(searchInput, { target: { value: "React" } });

    await waitFor(() => {
      expect(screen.getByText("React Post")).toBeInTheDocument();
      expect(screen.queryByText("Vue Post")).not.toBeInTheDocument();
    });
  });
});