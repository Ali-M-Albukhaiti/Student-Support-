import { Input } from "@/components/ui/Input";
import { Select, SelectTrigger, SelectValue, SelectContent, SelectItem } from "@/components/ui/Select";
import { Search } from "lucide-react";

export default function SearchFilters({
  searchQuery, setSearchQuery,
  semesterFilter, setSemesterFilter,
  studyProgramFilter, setStudyProgramFilter,
  studyPrograms
}) {
  return (
    <div className="grid grid-cols-1 md:grid-cols-4 gap-4 mb-6">
      <div className="md:col-span-2 relative">
        <Search className="absolute left-3 top-1/2 transform -translate-y-1/2 text-muted-foreground h-4 w-4" />
        <Input
          placeholder="Search posts..."
          className="pl-10"
          value={searchQuery}
          onChange={e => setSearchQuery(e.target.value)}
        />
      </div>

      <Select onValueChange={setStudyProgramFilter}>
        <SelectTrigger>
          <SelectValue placeholder="Study Program" />
        </SelectTrigger>
        <SelectContent>
          <SelectItem value="all">All Programs</SelectItem>
          {studyPrograms.map(program => (
            <SelectItem key={program} value={program}>{program}</SelectItem>
          ))}
        </SelectContent>
      </Select>

      <Select onValueChange={setSemesterFilter}>
        <SelectTrigger>
          <SelectValue placeholder="Semester" />
        </SelectTrigger>
        <SelectContent>
          <SelectItem value="all">All Semesters</SelectItem>
          {[...Array(8)].map((_, i) => (
            <SelectItem key={i + 1} value={`${i + 1}`}>Semester {i + 1}</SelectItem>
          ))}
        </SelectContent>
      </Select>
    </div>
  );
}
