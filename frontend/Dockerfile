# Use an official Node runtime as a parent image
FROM node:18-alpine

ARG API_URL

# Set the working directory to /app
WORKDIR /app

# Copy the package.json and package-lock.json to the working directory
COPY ./package.json ./
COPY ./package-lock.json ./

# Install the dependencies 
RUN npm install && npm install -g serve@14.2.3

# Copy the remaining application files to the working directory
COPY . .

# Set environment variable
ENV VITE_API_BASE_URL="http://${API_URL}:8081"

# Build the application
RUN npm run build

# Expose port 3000 for the application
EXPOSE 3000

# Start the application
CMD [ "npm", "run", "start" ]
