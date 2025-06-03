using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.RePurpose.Constants;
public static class Prompts
{
    public static string BlogPostPrompt = @"Objective: Convert the provided content into an engaging, SEO-optimized blog post that educates and resonates with the target audience.

Instructions:

Craft a Captivating Title:

Develop a headline that is clear, concise (ideally under 60 characters), and incorporates relevant keywords.

Consider using numbers or bracketed clarifications to increase engagement.
HubSpot Blog

Write a Compelling Introduction:

Begin with a hook—such as a question, statistic, or anecdote—to grab the reader's attention.

Clearly state what the reader will learn or gain from the post.

Organize Content with Subheadings:

Break the content into sections with descriptive H2 subheadings.

Ensure each section flows logically and covers a specific aspect of the topic.
HubSpot Blog
+2
HubSpot Blog
+2
HubSpot Blog
+2

Develop Informative Body Content:

Provide detailed explanations, insights, or instructions relevant to each subheading.

Use bullet points or numbered lists where appropriate to enhance readability.

Incorporate relevant data, examples, or quotes to support key points.
HubSpot Blog

Enhance with Visual Elements:

Suggest or include images, infographics, or videos that complement the text.

Ensure all multimedia elements are relevant and add value to the content.
HubSpot Blog

Conclude Effectively:

Summarize the main takeaways of the post.

Provide a clear call-to-action (CTA), such as encouraging comments, sharing the post, or exploring related resources.
HubSpot Blog

Optimize for SEO:

Integrate target keywords naturally throughout the post.

Write a meta description under 155 characters that accurately summarizes the content.

Use internal and external links to reputable sources where appropriate.
HubSpot Blog

Output: A well-structured blog post that aligns with best practices for readability, engagement, and search engine optimization.";

    public static string LinkedInPrompt = @"Objective:
Transform the given content into a concise, value-driven LinkedIn post that captures attention, sparks conversation, and builds professional credibility.

Instructions:

Hook with a Bold Opening Line:

Start with a strong opinion, surprising fact, relatable insight, or a provocative question.

Keep it short—ideally one sentence—and format it as a standalone paragraph to stop the scroll.

Add Context and Value:

In 2–4 short paragraphs, expand on the idea.

Share a story, insight, or lesson that makes the post informative or relatable.

Focus on one clear message or takeaway.

Engage with Authenticity:

Use a conversational, human tone—speak like you would to a colleague.

Don’t overuse buzzwords; instead, focus on clarity and real-world relevance.

Share your own perspective or experience when possible.

Invite Interaction:

End with a clear call-to-action (CTA) to prompt comments (e.g., “What’s your take?”, “Have you faced this?”, “Let me know below.”).

Ask open-ended questions to drive conversation.

Format for Readability:

Break text into short paragraphs or one-liners.

Use emojis or line breaks sparingly to guide the eye, not distract.

Optimize for Visibility:

Keep it under 300 words for best engagement.

Use 2–5 relevant hashtags to boost reach (#leadership, #marketingtips, etc.).

Avoid external links in the post body; if needed, add them in the first comment.

Output:
A scroll-stopping LinkedIn post that communicates value quickly, sparks engagement, and reflects your professional voice.";

    public static string TweetPrompt = @"Objective:
Condense the key message of the content into a punchy, high-impact tweet that informs, entertains, or provokes thought—within the 280-character limit.

Instructions:

Lead with Impact:

Start strong. Use a bold opinion, intriguing question, or unexpected fact.

Make the first few words count—they determine whether people stop scrolling.

Get to the Point:

Focus on one core idea or takeaway. Avoid stuffing in multiple messages.

Use clear, simple language; avoid jargon.

Add Emotion or Personality:

Make it human—funny, insightful, inspiring, or even a little controversial (tastefully).

Emojis can add flair but use them sparingly.

Use Smart Formatting:

Break long thoughts into separate tweets for a thread if needed.

Capitalize important words for emphasis, but don't overdo it.

Prompt Engagement:

Ask a quick question or invite a reaction (e.g., “Agree?”, “Thoughts?”, “Ever experienced this?”).

Calls to comment, like, or retweet can help boost visibility.

Include Visuals or Hashtags (Optional):

A relevant image, GIF, or chart can boost attention.

Use 1–2 well-chosen hashtags if topical (avoid hashtag overload).

Keep the length below 250 characters.

Output:
A single tweet (or concise thread) that distills the essence of the original content into a format that is engaging, scannable, and shareable.";
}
