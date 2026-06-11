import * as React from "react";
import { cva } from "class-variance-authority";
import { cn } from "@/lib/utils";

const inputVariants = cva(
  "flex h-10 w-full rounded-md border border-input bg-background px-3 py-2 text-sm placeholder:text-muted-foreground focus:outline-none focus:ring-2 focus:ring-ring focus:ring-offset-2 disabled:opacity-50 disabled:cursor-not-allowed",
  {
    variants: {
      size: {
        default: "h-10 px-3",
        sm: "h-9 px-2",
        lg: "h-12 px-4",
      },
      variant: {
        default: "border-input bg-background text-foreground",
        outline: "border border-input bg-background",
      },
    },
    defaultVariants: {
      size: "default",
      variant: "default",
    },
  }
);

const Input = React.forwardRef(({ className, size, variant, ...props }, ref) => (
  <input className={cn(inputVariants({ size, variant, className }))} ref={ref} {...props} />
));
Input.displayName = "Input";

export { Input, inputVariants };
