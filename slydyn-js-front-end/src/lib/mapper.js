// @ts-check

/**
 * @template T
 * @param {any} source
 * @param {import('zod').ZodSchema<T>} destinationSchema
 */
export function mapToDto(source, destinationSchema) {
  const dto = destinationSchema.parse(source);
  return dto;
}

/**
 * @template T
 * @param {any} source
 * @param {import('zod').ZodSchema<T>} destinationSchema
 */
export function mapToDtoSafe(source, destinationSchema) {
  try {
    const dto = destinationSchema.parse(source);
    return dto;
  } catch (error) {
    return null;
  }
}
