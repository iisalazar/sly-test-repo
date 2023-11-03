// @ts-check
import { z } from "zod";

export const UserSchemaShape = {
  id: z.string(),
  email: z.string().email(),
  userName: z.string(),
  firstName: z.string().nullable(),
  lastName: z.string().nullable(),
};

export const UserSchema = z.object(UserSchemaShape);

/**
 * @typedef {z.infer<typeof UserSchema>} UserDto
 */

export {};
