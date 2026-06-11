import { useEffect, useState } from "react";
import { Button } from "@/components/ui/Button";
import { Card, CardHeader, CardContent } from "@/components/ui/Card";
import { Badge } from "@/components/ui/Badge";
import { Plus, Heart, MessageCircle, Trophy, User, Calendar } from "lucide-react";
import api from "@/services/api.js";

import CreatePostForm from "@/components/ui/posts/CreatePostForm";
import EditPostForm from "@/components/ui/posts/EditPostForm";
import SearchFilters from "@/components/ui/posts/SearchFilters";

function PostsPage() {
  const [posts, setPosts] = useState([]);
  const [filteredPosts, setFilteredPosts] = useState([]);
  const [searchQuery, setSearchQuery] = useState("");
  const [semesterFilter, setSemesterFilter] = useState("all");
  const [studyProgramFilter, setStudyProgramFilter] = useState("all");
  const [studyPrograms, setStudyPrograms] = useState([]);

  const [showCreateForm, setShowCreateForm] = useState(false);
  const [editingPost, setEditingPost] = useState(null);

  const authorId = 1; // logged-in user ID

  // Fetch posts
  const fetchPosts = async () => {
    try {
      const response = await api.get("/posts");
      setPosts(response.data);
      setFilteredPosts(response.data);

      // Extract unique study programs
      const programs = Array.from(new Set(response.data.map(p => p.author?.studyProgram)));
      setStudyPrograms(programs);
    } catch (error) {
      console.error("Error fetching posts:", error);
    }
  };

  useEffect(() => {
    fetchPosts();
  }, []);

  // Filter posts
  useEffect(() => {
    let filtered = [...posts];
    if (searchQuery)
      filtered = filtered.filter(p =>
        p.title.toLowerCase().includes(searchQuery.toLowerCase()) ||
        p.content.toLowerCase().includes(searchQuery.toLowerCase())
      );
    if (semesterFilter !== "all")
      filtered = filtered.filter(p => p.semester === parseInt(semesterFilter));
    if (studyProgramFilter !== "all")
      filtered = filtered.filter(p => p.author?.studyProgram === studyProgramFilter);
    setFilteredPosts(filtered);
  }, [searchQuery, semesterFilter, studyProgramFilter, posts]);

  // Create post handler
  const handleCreatePost = async () => {
    await fetchPosts(); // fetch latest posts from backend after creation
  };

  // Edit post
  const handleEditPost = (post) => setEditingPost(post);

  const handleUpdatePost = async (updatedPost) => {
    try {
      const payload = {
        id: updatedPost.id,
        title: updatedPost.title,
        content: updatedPost.content,
        semester: updatedPost.semester,
        authorId: updatedPost.authorId || updatedPost.author?.id,
        author: null,
      };
      const response = await api.put(`/posts/${updatedPost.id}`, payload);
      const updatedPostsArray = posts.map(p =>
        p.id === updatedPost.id ? response.data : p
      );
      setPosts(updatedPostsArray);
      setFilteredPosts(updatedPostsArray);
      setEditingPost(null);
    } catch (error) {
      console.error("Error updating post:", error);
      alert("Failed to update post.");
    }
  };

  // Delete post
  const handleDeletePost = async (postId) => {
    if (!window.confirm("Are you sure you want to delete this post?")) return;
    try {
      await api.delete(`/posts/${postId}`);
      const updatedPosts = posts.filter(p => p.id !== postId);
      setPosts(updatedPosts);
      setFilteredPosts(updatedPosts);
    } catch (error) {
      console.error("Error deleting post:", error);
      alert("Failed to delete post.");
    }
  };

  return (
    <div className="min-h-screen bg-muted/20 p-4">
      {/* Header */}
      <div className="flex flex-col lg:flex-row lg:items-center lg:justify-between gap-4 mb-6">
        <div>
          <h1 className="text-3xl font-bold">Student Posts</h1>
          <p className="text-muted-foreground">
            Discover semester experiences, tips, and insights from fellow students
          </p>
        </div>
        <Button
          variant="purple"
          className="bg-purple-600 hover:bg-purple-700 text-white"
          onClick={() => setShowCreateForm(!showCreateForm)}
        >
          <Plus className="h-4 w-4 mr-2" />
          Share Your Experience
        </Button>
      </div>

      {/* Create Form */}
      {showCreateForm && (
        <CreatePostForm
          authorId={authorId}
          handleCreate={handleCreatePost}
          closeForm={() => setShowCreateForm(false)}
        />
      )}

      {/* Edit Form */}
      {editingPost && (
        <EditPostForm
          post={editingPost}
          handleUpdate={handleUpdatePost}
          closeForm={() => setEditingPost(null)}
        />
      )}

      {/* Search & Filters */}
      <SearchFilters
        searchQuery={searchQuery}
        setSearchQuery={setSearchQuery}
        semesterFilter={semesterFilter}
        setSemesterFilter={setSemesterFilter}
        studyProgramFilter={studyProgramFilter}
        setStudyProgramFilter={setStudyProgramFilter}
        studyPrograms={studyPrograms}
      />

      {/* Posts Grid */}
      <div className="grid gap-6">
        {filteredPosts.length === 0 && <p className="p-4">No posts found.</p>}
        {filteredPosts.map(post => (
          <Card key={post.id} className="hover:shadow-lg transition-all border-0">
            <CardHeader className="pb-3">
              <div className="flex items-start justify-between">
                <div className="flex-1">
                  <h3 className="text-xl font-semibold mb-2 hover:text-primary transition-colors cursor-pointer">
                    {post.title}
                  </h3>
                  <p className="text-muted-foreground line-clamp-2">{post.content}</p>
                </div>
                <div className="flex items-center space-x-2">
                  <Badge variant="secondary">
                    <Trophy className="h-3 w-3 mr-1" />
                    {post.author?.points || 0} pts
                  </Badge>
                  {post.author?.id === authorId && (
                    <>
                      <Button size="sm" variant="outline" onClick={() => handleEditPost(post)}>
                        Edit
                      </Button>
                      <Button size="sm" variant="destructive" onClick={() => handleDeletePost(post.id)}>
                        Delete
                      </Button>
                    </>
                  )}
                </div>
              </div>
            </CardHeader>
            <CardContent className="pt-0">
              <div className="flex items-center justify-between">
                <div className="flex items-center space-x-4 text-sm text-muted-foreground">
                  <div className="flex items-center space-x-2">
                    <div className="w-8 h-8 bg-primary/10 rounded-full flex items-center justify-center">
                      <User className="h-4 w-4 text-primary" />
                    </div>
                    <span className="font-medium">{post.author?.fullName}</span>
                  </div>
                  <Badge variant="outline" className="text-xs">
                    {post.author?.studyProgram} • Semester {post.semester}
                  </Badge>
                  <div className="flex items-center space-x-1">
                    <Calendar className="h-3 w-3" />
                    <span>{new Date(post.createdAt).toLocaleDateString()}</span>
                  </div>
                </div>
                <div className="flex items-center space-x-4 text-sm text-muted-foreground">
                  <div className="flex items-center space-x-1 hover:text-red-500 cursor-pointer transition-colors">
                    <Heart className="h-4 w-4" />
                    <span>{post.postLikes?.length || 0}</span>
                  </div>
                  <div className="flex items-center space-x-1 hover:text-primary cursor-pointer transition-colors">
                    <MessageCircle className="h-4 w-4" />
                    <span>{post.comments?.length || 0}</span>
                  </div>
                </div>
              </div>
            </CardContent>
          </Card>
        ))}
      </div>
    </div>
  );
}

export default PostsPage;
