Sep 2, 2008
README for ShaderEffectTemplate project, sources and accompanying documentation

IMPORTANT: * Make sure that the shader effect is saved into unicode without signature (code page 65001). 
Choose File | Save As, then click the dropdown arrow next to the Save button and select "Save with Encoding"
By default VS will add signature the first time a file is created, which will cause pixel shading compilation to fail.

* To add new shader effect:
1. create new project folder for it
2. add the .fx and .cs files
3. change CompileEffects.bat to enable compilation for the new effect
4. Build to compile the effect the first time
5. Add the compiled .ps file to the project, set its Build Action to Resource from the Properties window 

* To use shader effects from XAML:
see sample in Window1.xaml

* there is a prebuild event calling $(ProjectDir)\CompileEffects.bat $(ProjectDir) - see project property pages for details

* more information and contacts:
http://blogs.msdn.com/nikola
http://nokola.com
Created by Nikola Mihaylov

-------------------------------------------------------------------------------------------------------------------------
DISCLAIMER: THE SOURCE CODE IN THIS PROJECT AND RELATED DOCUMENTATION AND LINKS ARE PROVIDED 'AS IS' 
AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY 
AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE REGENTS OR CONTRIBUTORS BE LIABLE 
FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED 
TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) 
HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING 
NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE. 

license: the accompanying source code, project and documentation are governed by the use of MS-PL license.
Read license.txt for more details, or visit http://www.opensource.org/licenses/ms-pl.html for the latest version of the license
