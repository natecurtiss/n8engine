using System.Numerics;
using OpenGLTutorials;
using Silk.NET.OpenGL;
using Boolean = Silk.NET.OpenGL.Boolean;

new W().Start();

sealed class W : Base, WindowSize
{
    GL _gl = null!;
    uint _vertexBufferObject;
    uint _vertexArrayObject;
    Shader _shader = null!;
    Camera _camera = null!;
    float _time;

    const float ROTATION_SPEED = 100f;
    
    // Run on each vertex.
    readonly string _vertexShaderSource = @"
        #version 330 core // Using version GLSL version 3.3
        layout (location = 0) in vec2 aPosition;
        layout (location = 1) in vec3 aColor;
        out vec4 vertexColor;

        uniform mat4 projection;
        uniform mat4 model;

        void main() 
        {
            vertexColor = vec4(aColor.rgb, 1.0);
            gl_Position = projection * model * vec4(aPosition.xy, 0, 1.0);
        }";

    // Run on each fragment/pixel of the geometry.
    readonly string _fragmentShaderSource = @"
        #version 330 core
        out vec4 FragColor;
        in vec4 vertexColor;

        void main() 
        {
            FragColor = vertexColor;
        }";

    // https://www.youtube.com/watch?v=hrZbyd4qPnk
    // https://github.com/dotnet/Silk.NET/blob/main/examples/CSharp/OpenGL%20Tutorials/Tutorial%201.2%20-%20Hello%20quad/Program.cs
    protected override unsafe void OnLoad()
    {
        base.OnLoad();
        // Get the OpenGL API for drawing to the window.
        _gl = GL.GetApi(Window);
        
        _shader = new(_gl, _vertexShaderSource, _fragmentShaderSource);
        _shader.Load();
        
        // Create the vertex array object (VBA; array of VBOs).
        _vertexArrayObject = _gl.GenVertexArray();
        _gl.BindVertexArray(_vertexArrayObject);
        
        // Create the vertex buffer object (VBO) that holds the vertex data.
        _vertexBufferObject = _gl.GenBuffer();
        _gl.BindBuffer(BufferTargetARB.ArrayBuffer, _vertexBufferObject);
        
        // Vertex data; uploaded to the VBO.
        var vertices = new[]
        {
            -0.5f, 0.5f, 1f, 0f, 0f, // Top left.
            0.5f, 0.5f, 0f, 1f, 0f, // Top right.
            -0.5f, -0.5f, 0f, 0f, 1f, // Bottom left.

            0.5f, 0.5f, 0f, 1f, 0f, // Top right.
            0.5f, -0.5f, 0f, 1f, 1f, // Bottom right.
            -0.5f, -0.5f, 0f, 0f, 1f, // Bottom left.
        };
        
        fixed (void* pointerToFirstVertex = &vertices[0])
        {
            // Fill the array buffer we binded to before.
            // Number of bytes the data is.
            // Pointer to the first vertex.
            // Draw.
            _gl.BufferData(GLEnum.ArrayBuffer, (nuint) (sizeof(nuint) * vertices.Length), pointerToFirstVertex, GLEnum.StaticDraw);
        }
        
        // *** We're using inter-leaf data (X, Y, R, G, and B are included in the same array). ***
        
        // This means for positions we need to count the first two and then skip the rest in the same line.
        // Start at index 0.
        // Positions -> 2 dimensions.
        // We're using floats.
        // Don't normalize the values.
        // Amount of bytes (X, Y, R, G, B) between first and last floats (skip this many bytes after before you read the first two and then read those first two).
        // X and Y positions are the first thing in the array (don't offset it).
        _gl.VertexAttribPointer(0, 2, GLEnum.Float, Boolean.False, 5 * sizeof(float), null);
        // Enable the first vertex attrib array for positions.
        _gl.EnableVertexAttribArray(0);
        
        // Now we move on to colors.
        // Start at the second index (after X and Y).
        // We're using floats.
        // Don't normalize the values.
        // There are 5 values between each line (number of bytes).
        // Offset this by two when we "skip" because our first two values of each line (every 5) are X and Y.
        _gl.VertexAttribPointer(1, 3, GLEnum.Float, Boolean.False, 5 * sizeof(float), (void*) (2 * sizeof(float)));
        _gl.EnableVertexAttribArray(1);
        
        // Unbind both buffers now that we're done using them.
        _gl.BindVertexArray(0);
        _gl.BindBuffer(BufferTargetARB.ArrayBuffer, 0);

        _camera = new(Vector2.Zero, 1f, this);
    }

    protected override void OnRender(double dt)
    {
        _time += (float) dt;
        
        // Clear the screen and set to black.
        _gl.ClearColor(0, 0, 0, 0);
        _gl.Clear(ClearBufferMask.ColorBufferBit);

        var position = new Vector2(0f, 0f);
        var scale = new Vector2(150, 100);
        var rotation = _time * (MathF.PI / 180f) * ROTATION_SPEED;
        var translationMatrix = Matrix4x4.CreateTranslation(position.X, position.Y, 0f);
        var scaleMatrix = Matrix4x4.CreateScale(scale.X, scale.Y, 1f);
        var rotationMatrix = Matrix4x4.CreateRotationZ(rotation);
        
        _shader.SetMatrix("model", scaleMatrix * rotationMatrix * translationMatrix);
        _shader.Use();
        _shader.SetMatrix("projection", _camera.ProjectionMatrix());
        
        // Bind the vertex array object, use the shader, draw the triangles, unbind the vertex array object.
        _gl.BindVertexArray(_vertexArrayObject);
        _gl.DrawArrays(PrimitiveType.Triangles, 0, 6);
        _gl.BindVertexArray(0);
    }
}